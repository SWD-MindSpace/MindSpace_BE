using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Domain.Entities.Tests
{
    public class Test : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int QuestionCount { get; set; }
        public decimal Price { get; set; }
        public TestStatus TestStatus { get; set; }

        //Relationships
        public int TestCategoryId { get; set; }
        public virtual TestCategory TestCategory { get; set; }
        public virtual IEnumerable<TestTestQuestion> TestTestQuestions { get; set; } = [];
        public virtual IEnumerable<TestResponse> TestResponses { get; set; } = [];
        public int ManagerId { get; set; }
        public virtual ApplicationUser Manager { get; set; }
    }
}