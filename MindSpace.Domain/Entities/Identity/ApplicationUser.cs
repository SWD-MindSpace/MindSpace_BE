using Microsoft.AspNetCore.Identity;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.MindSpace.Domain.Entities;
using MindSpace.Domain.Entities.Tests;

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
        public virtual IEnumerable<TestResponse>? TestResponses { get; set; } = [];

        public virtual IEnumerable<Test> Tests { get; set; } = [];

        // 1 Psychologist - 1 User
        public virtual Psychologist Psychologist { get; set; }

        public virtual Manager Manager { get; set; }

        public virtual Student Student { get; set; }
    }
}