using System.Runtime.Serialization;

namespace MindSpace.Domain.Entities.Constants
{
    public enum QuestionFormats
    {
        [EnumMember(Value = "MultipleChoice")]
        MultipleChoice,
        [EnumMember(Value = "Text")]
        Text,
        [EnumMember(Value = "Dropdown")]
        Dropdown
    }
}
