﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.RequestHelpers;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Application.Features.ApplicationUsers.Commands.ToggleAccountStatus;
using MindSpace.Application.Features.ApplicationUsers.Commands.UpdatePsychologistCommissionRate;
using MindSpace.Application.Features.ApplicationUsers.Commands.UserUpdateProfile;
using MindSpace.Application.Features.ApplicationUsers.Queries.GetAllAccounts;
using MindSpace.Application.Features.ApplicationUsers.Queries.GetAllPsychologists;
using MindSpace.Application.Features.ApplicationUsers.Queries.GetAllPsychologistsNames;
using MindSpace.Application.Features.ApplicationUsers.Queries.GetAllStudents;
using MindSpace.Application.Features.ApplicationUsers.Queries.GetMyProfile;
using MindSpace.Application.Features.ApplicationUsers.Queries.GetProfileById;
using MindSpace.Application.Features.ApplicationUsers.Queries.GetPsychologistById;
using MindSpace.Application.Features.Authentications.Commands.ConfirmEmail;
using MindSpace.Application.Features.Authentications.Commands.LoginUser;
using MindSpace.Application.Features.Authentications.Commands.LogoutUser;
using MindSpace.Application.Features.Authentications.Commands.RefreshUserAccessToken;
using MindSpace.Application.Features.Authentications.Commands.RegisterForUser.RegisterManager;
using MindSpace.Application.Features.Authentications.Commands.RegisterForUser.RegisterParent;
using MindSpace.Application.Features.Authentications.Commands.RegisterForUser.RegisterPsychologist;
using MindSpace.Application.Features.Authentications.Commands.RegisterForUser.RegisterStudent;
using MindSpace.Application.Features.Authentications.Commands.ResetPassword;
using MindSpace.Application.Features.Authentications.Commands.RevokeUser;
using MindSpace.Application.Features.Authentications.Commands.SendEmailConfirmation;
using MindSpace.Application.Features.Authentications.Commands.SendResetPasswordEmail;
using MindSpace.Application.Specifications.ApplicationUserSpecifications;
using MindSpace.Application.Specifications.PsychologistsSpecifications;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Exceptions;
using System.IdentityModel.Tokens.Jwt;

namespace MindSpace.API.Controllers
{
    [Route("api/v{version:apiVersion}/identities")]
    public class IdentitiesController(
        IMediator mediator,
        UserManager<ApplicationUser> userManager) : BaseApiController
    {
        // ====================================
        // === CREATE, PATCH, DELETE, PUT
        // ====================================

        // POST /api/v1/identities/register
        // Register a new parent account
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] RegisterParentCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        // POST /api/v1/identities/login
        // Login user and get access token
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginUserCommand command)
        {
            LoginResponseDTO response;
            try
            {
                response = await mediator.Send(command);
            }
            catch (DuplicateUserException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        // POST /api/v1/identities/logout
        // Logout user and invalidate refresh token
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

        // POST /api/v1/identities/refresh
        // Refresh access token using refresh token
        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<ActionResult<RefreshUserAccessTokenDTO>> Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized("Refresh token is required");
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(refreshToken);
            // 3 seconds clock skew, this is to account for the time it takes for the token to reach the server
            if (jwtToken.ValidTo < DateTime.Now.AddSeconds(3))
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

        // POST /api/v1/identities/revoke/{id}
        // Revoke refresh token for a user (Admin only)
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

        // POST /api/v1/identities/register-for/manager
        // Register new school managers
        [HttpPost("register-for/manager")]
        //[InvalidateCache("/api/identities/accounts|")]
        public async Task<IActionResult> RegisterSchoolManager([FromForm] RegisterSchoolManagerCommand command)
        {
            await mediator.Send(command);
            return Ok("All managers have been added");
        }

        // POST /api/v1/identities/register-for/psychologist
        // Register new psychologists (Admin only)
        [HttpPost("register-for/psychologist")]
        [Authorize(Roles = UserRoles.Admin)]
        //[InvalidateCache("/api/identities/accounts|")]
        public async Task<IActionResult> RegisterPsychologist([FromForm] RegisterPsychologistCommand command)
        {
            await mediator.Send(command);
            return Ok("All psychologists have been added");
        }

        // POST /api/v1/identities/register-for/student
        // Register new students (SchoolManager only)
        [HttpPost("register-for/student")]
        //[InvalidateCache("/api/identities/accounts|")]
        [Authorize(Roles = UserRoles.SchoolManager)]
        public async Task<IActionResult> RegisterStudent([FromForm] RegisterStudentsCommand command)
        {
            await mediator.Send(command);
            return Ok("Student registered successfully");
        }

        // POST /api/v1/identities/send-email-confirmation
        // Send email confirmation to authenticated user
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

        // POST /api/v1/identities/confirm-email
        // Confirm user's email address
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

        // POST /api/v1/identities/send-reset-password-email
        // Send password reset email
        [HttpPost("send-reset-password-email")]
        [AllowAnonymous]
        public async Task<IActionResult> SendResetPasswordEmail([FromBody] SendResetPasswordEmailCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        // POST /api/v1/identities/reset-password
        // Reset user's password
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

        // PUT /api/v1/identities/profile
        // Update authenticated user's profile
        // [InvalidateCache("/api/identities/profile|")]
        // PUT /api/identities/profile
        [HttpPut("profile")]
        [Authorize]
        public async Task<ActionResult<ApplicationUserProfileDTO>> UserUpdateProfile([FromForm] UserUpdateProfileCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        // PUT /api/v1/identities/psychologists/{id}/commission-rate
        // Update psychologist's commission rate
        // [InvalidateCache("/api/identities/accounts|")]
        [HttpPut("psychologists/{id}/commission-rate")]
        [Authorize]
        public async Task<ActionResult<ApplicationUserProfileDTO>> UpdatePsychologistCommissionRate([FromRoute] int id, [FromBody] UpdatePsychologistCommissionRateCommand command)
        {
            command.PsychologistId = id;
            await mediator.Send(command);
            return NoContent();
        }

        // PUT /api/v1/identities/accounts/{id}/toggle-status
        // Toggle account status (Admin and SchoolManager only)
        //[InvalidateCache("/api/identities/accounts|")]
        [HttpPut("accounts/{id}/toggle-status")]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.SchoolManager}")]
        public async Task<IActionResult> ToggleAccountStatus([FromRoute] int id)
        {
            await mediator.Send(new ToggleAccountStatusCommand { UserId = id });
            return NoContent();
        }

        // ==============================
        // === GET
        // ==============================

        // GET /api/v1/identities/profile
        // Get authenticated user's profile
        //[Cache(600)]
        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<ApplicationUserProfileDTO>> GetProfile()
        {
            var result = await mediator.Send(new GetMyProfileQuery());
            return Ok(result);
        }

        // GET /api/v1/identities/profile/{id}
        // Get user profile by ID (Admin and SchoolManager only)
        //[Cache(600)]
        [HttpGet("profile/{id}")]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.SchoolManager}")]
        public async Task<ActionResult<ApplicationUserProfileDTO>> GetProfileById(int id)
        {
            var result = await mediator.Send(new GetProfileByIdQuery { UserId = id });
            return Ok(result);
        }

        // GET /api/v1/identities/accounts
        // Get all accounts with pagination and filtering (Admin and SchoolManager only)
        //[Cache(30000)]
        [HttpGet("accounts")]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.SchoolManager}")]
        public async Task<ActionResult<Pagination<ApplicationUserProfileDTO>>> GetAllAccounts([FromQuery] ApplicationUserSpecParams specParams)
        {
            var result = await mediator.Send(new GetAllAccountsQuery(specParams));
            return PaginationOkResult(
                result.Data,
                result.Count,
                specParams.PageIndex,
                specParams.PageSize
            );
        }

        // GET /api/v1/identities/accounts/students
        // Get all students with pagination and filtering (SchoolManager only)
        //[Cache(30000)]
        [HttpGet("accounts/students")]
        [Authorize(Roles = UserRoles.SchoolManager)]
        public async Task<ActionResult<Pagination<ApplicationUserProfileDTO>>> GetAllStudents([FromQuery] ApplicationUserSpecParams specParams)
        {
            var result = await mediator.Send(new GetAllStudentsQuery(specParams));
            return PaginationOkResult(
                result.Data,
                result.Count,
                specParams.PageIndex,
                specParams.PageSize
            );
        }

        // GET /api/v1/identities/accounts/psychologists
        // Get all psychologists with pagination and filtering
        //[Cache(30000)]
        [HttpGet("accounts/psychologists")]
        public async Task<ActionResult<Pagination<PsychologistProfileDTO>>> GetAllPsychologists([FromQuery] PsychologistSpecParams specParams)
        {
            var result = await mediator.Send(new GetAllPsychologistsQuery(specParams));
            return PaginationOkResult(
                result.Data,
                result.Count,
                specParams.PageIndex,
                specParams.PageSize
            );
        }

        // GET /api/v1/identities/accounts/psychologists/names
        // Get list of all psychologist names
        //[Cache(30000)]
        [HttpGet("accounts/psychologists/names")]
        [Authorize]
        public async Task<ActionResult<List<string>>> GetAllPsychologistsNames()
        {
            var result = await mediator.Send(new GetAllPsychologistsNamesQuery());
            return Ok(result);
        }

        // GET /api/v1/identities/accounts/psychologists/{id}
        // Get psychologist profile by ID
        //[Cache(30000)]
        [HttpGet("accounts/psychologists/{id}")]
        [Authorize]
        public async Task<ActionResult<PsychologistProfileDTO>> GetPsychologistById([FromRoute] int id)
        {
            var result = await mediator.Send(new GetPsychologistByIdQuery(id));
            return Ok(result);
        }
    }
}