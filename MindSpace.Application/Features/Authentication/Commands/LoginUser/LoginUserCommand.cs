﻿using MediatR;
using MindSpace.Application.DTOs.ApplicationUsers;

namespace MindSpace.Application.Features.Authentication.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<LoginResponseDTO>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}