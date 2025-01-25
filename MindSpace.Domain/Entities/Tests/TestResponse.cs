using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Domain.Entities.Tests
{
    public class TestResponse : BaseEntity
    {
        public int TotalScore { get; set; }

        //Relationships
        public int StudentId { get; set; }
        public ApplicationUser Student { get; set; }

        public int TestId { get; set; }
        public virtual Test Test { get; set; }
    }
}