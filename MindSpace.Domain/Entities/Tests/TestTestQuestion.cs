namespace MindSpace.Domain.Entities.Tests
{
    public class TestTestQuestion
    {
        //Relationships
        public int TestId { get; set; }
        public virtual Test Test { get; set; }

        public int TestQuestionId { get; set; }
        public virtual TestQuestion TestQuestion { get; set; }
    }
}