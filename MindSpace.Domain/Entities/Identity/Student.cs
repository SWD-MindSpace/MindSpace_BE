using MindSpace.Domain.Entities.MindSpace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities.Identity
{
    public class Student : ApplicationUser
    {
        public virtual ApplicationUser User { get; set; }

        public int SchoolId { get; set; }
        public virtual School School { get; set; }
    }
}
