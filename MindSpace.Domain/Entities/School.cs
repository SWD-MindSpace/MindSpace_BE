using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Entities.Owned;

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
            public int SchoolManagerId { get; set; }
            public virtual Manager SchoolManager { get; set; }
        }
    }

}
