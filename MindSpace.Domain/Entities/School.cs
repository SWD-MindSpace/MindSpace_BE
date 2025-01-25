﻿using MindSpace.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities
{
    namespace MindSpace.Domain.Entities
    {
        public class School : BaseEntity
        {
            public string SchoolName { get; set; }
            public string ContactEmail { get; set; }
            public string PhoneNumber { get; set; }
            public Address Address { get; set; }
            public string? Description { get; set; }

            public DateOnly JoinDate { get; set; }

            //Navigation props
            public virtual IEnumerable<Student> Students { get; set; } = [];

            // 1 School - 1 School Manager
            public int SchoolManagerId { get; set; }
            public virtual SchoolManager SchoolManager { get; set; }

            // 1 School - 1 SupportingPrograms
            public virtual ICollection<SupportingProgram> SupportingPrograms { get; set; } = new HashSet<SupportingProgram>();
        }
    }

}
