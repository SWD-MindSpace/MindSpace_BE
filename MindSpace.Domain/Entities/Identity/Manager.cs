using MindSpace.Domain.Entities.MindSpace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities.Identity
{
    public class Manager : ApplicationUser
    {
        // 1 Manager - 1 User 
        public virtual ApplicationUser User { get; set; }

        // 1 Manager - 1 School
        public int SchoolId { get; set; }
        public virtual School School { get; set; }

        // 1 Manager - M SupportingPrograms
        public virtual IEnumerable<SupportingProgram> SupportingPrograms { get; set; }
    }
}
