﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.Features.Authentication.Commands.LoginUser;
using MindSpace.Application.Features.Authentication.Commands.LogoutUser;
using MindSpace.Application.Features.Authentication.Commands.RefreshUserAccessToken;
using MindSpace.Application.Features.Authentication.Commands.RegisterForUser.RegisterManager;
using MindSpace.Application.Features.Authentication.Commands.RegisterForUser.RegisterParent;
using MindSpace.Application.Features.Authentication.Commands.RegisterForUser.RegisterPsychologist;
using MindSpace.Application.Features.Authentication.Commands.RegisterForUser.RegisterStudent;
using MindSpace.Application.Features.Authentication.Commands.RevokeUser;
using MindSpace.Application.Features.Authentication.Commands.SendEmailConfirmation;
using MindSpace.Application.Features.Authentication.Commands.ConfirmEmail;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
using MindSpace.Application.Features.Authentication.Commands.SendResetPasswordEmail;
using MindSpace.Application.Features.Authentication.Commands.ResetPassword;

namespace MindSpace.API.Controllers
{
    public class IdentityController(
        IMediator mediator,
        UserManager<ApplicationUser> userManager) : BaseApiController
    {
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] RegisterParentCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] LoginUserCommand command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            await mediator.Send(new LogoutUserCommand
            {
                Response = Response
            });

            Response.Cookies.Delete("refreshToken");
            return NoContent();
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<ActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized("Refresh token is required");
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(refreshToken);
            // 3 seconds clock skew, this is to account for the time it takes for the token to reach the server
            if (jwtToken.ValidTo < DateTime.UtcNow.AddSeconds(3))
            {
                return Unauthorized("Expired refresh token");
            }

            var user = await userManager.FindByIdAsync(jwtToken.Subject);
            if (user == null || user.RefreshToken == null || !user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid refresh token");
            }

            var newTokens = await mediator.Send(new RefreshUserAccessTokenCommand(user));
            return Ok(newTokens);
        }

        [HttpPost("revoke/{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> RevokeRefreshToken(string id)
        {
            try
            {
                await mediator.Send(new RevokeUserCommand { UserId = id });
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("User not found");
            }
        }

        // Admin registers account for School Manager
        [HttpPost("register-for/manager")]
        public async Task<IActionResult> RegisterSchoolManager([FromForm] RegisterSchoolManagerCommand command)
        {
            await mediator.Send(command);
            return Ok("All managers have been added");
        }

        // Admin registers account for Psychologist
        [HttpPost("register-for/psychologist")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> RegisterPsychologist([FromForm] RegisterPsychologistCommand command)
        {
            await mediator.Send(command);
            return Ok("All psychologists have been added");
        }

        // School Manager registers account for students
        [HttpPost("register-for/student")]
        [Authorize(Roles = UserRoles.SchoolManager)]
        public async Task<IActionResult> RegisterStudent([FromForm] RegisterStudentsCommand command)
        {
            await mediator.Send(command);
            return Ok("Student registered successfully");
        }

        [HttpPost("send-email-confirmation")]
        [Authorize]
        public async Task<IActionResult> SendEmailConfirmation()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Logic to send email confirmation
            await mediator.Send(new SendEmailConfirmationCommand(user));
            return NoContent();
        }

        [HttpPost("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailCommand command)
        {
            var result = await mediator.Send(command);
            if (result)
            {
                return Ok("Email confirmed successfully");
            }
            return BadRequest("Email confirmation failed");
        }

        [HttpPost("send-reset-password-email")]
        [AllowAnonymous]
        public async Task<IActionResult> SendResetPasswordEmail([FromBody] SendResetPasswordEmailCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            var result = await mediator.Send(command);
            if (result)
            {
                return Ok("Password reset successfully");
            }
            return BadRequest("Password reset failed");
        }
    }
}