namespace MindSpace.Domain.Entities.Tests
{
    public class TestCategory : BaseEntity
    {
        public string Name { get; set; }

        //Navigation props
        public virtual IEnumerable<Test> Tests { get; set; }
    }
}