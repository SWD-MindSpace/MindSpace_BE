﻿namespace MindSpace.Application.Features.Authentication.DTOs
{
    public class RegisterUserDTO
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}