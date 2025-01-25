using Microsoft.AspNetCore.Identity;
using MindSpace.Domain.Commons.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindSpace.Domain.Entities
{
    public class ApplicationUser : IdentityUser<int> //Using int as key type
    {
        public string? FullName { get; set; }
        public string? ImageUrl { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserStatus Status { get; set; }

    }
}