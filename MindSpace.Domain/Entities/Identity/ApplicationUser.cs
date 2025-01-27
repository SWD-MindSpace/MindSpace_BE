﻿using Microsoft.AspNetCore.Identity;
using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<int> //Using int as key type
    {
        public string? FullName { get; set; }
        public string? ImageUrl { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserStatus Status { get; set; }

        //Navigation props

        // 1 Psychologist - 1 User
        public virtual Psychologist Psychologist { get; set; }

        // 1 Student - 1 User
        public virtual Student Student { get; set; }

        // 1 Manager - 1 User
        public virtual Manager Manager { get; set; }
    }
}