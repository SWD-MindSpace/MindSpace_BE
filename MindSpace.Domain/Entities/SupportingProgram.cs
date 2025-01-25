using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Entities.Owned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities
{
    public class SupportingProgram : BaseEntity
    {
        public int ManagerId { get; set; }
        public virtual Manager Manager { get; set; }
        public int PsychologistId { get; set; }
        public virtual Psychologist Psychologist { get; set; }

        public Address Address { get; set; }
    }
}