using MindSpace.Domain.Entities.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities
{
    public class Specification : BaseEntity
    {
        public string Name { get; set; }


        // 1 Specification - M Psychologists
        public virtual ICollection<Psychologist> Psychologists { get; set; } = new HashSet<Psychologist>();
    }
}
