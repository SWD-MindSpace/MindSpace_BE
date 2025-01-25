﻿using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Entities.Owned;

namespace MindSpace.Domain.Entities
{
    public class SupportingProgram : BaseEntity
    {
        // Fields
        public string ThumbnailUrl { get; set; }
        public string PdffileUrl { get; set; }
        public int MaxQuantity { get; set; }
        public Address Address { get; set; }
        public DateTime StartDateAt { get; set; }


        // 1 SchoolManager - M SupportingProgram
        public int ManagerId { get; set; }
        public virtual SchoolManager SchoolManager { get; set; }


        // 1 Psychologist - M SupportingProgram
        public int PsychologistId { get; set; }
        public virtual Psychologist Psychologist { get; set; }


        // 1 School - M Supporting Program
        public int SchoolId { get; set; }
        public virtual School School { get; set; }


        // M Students - M Supporting Program
        public ICollection<SupportingProgramHistory> SupportingProgramHistory { get; set; } = new HashSet<SupportingProgramHistory>();
    }
}