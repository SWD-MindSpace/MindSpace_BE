﻿using MindSpace.Domain.Entities;

namespace MindSpace.Application.DTOs.ApplicationUsers
{
    public class PsychologistProfileDTO : ApplicationUserProfileDTO
    {
        public string Bio { get; set; }
        public float AverageRating { get; set; }
        public decimal SessionPrice { get; set; }
        public decimal ComissionRate { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();
    }
}