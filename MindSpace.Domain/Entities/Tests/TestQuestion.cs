using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Domain.Entities.Tests
{
    public class TestQuestion : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public QuestionFormats QuestionFormat { get; set; }

        //Relationships
        public virtual IEnumerable<TestTestQuestion> TestTestQuestions { get; set; } = [];
    }
}