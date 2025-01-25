using System.Runtime.Serialization;

namespace MindSpace.Domain.Entities.Constants
{
    public enum TestStatus
    {
        [EnumMember(Value = "Enabled")]
        Enabled,

        [EnumMember(Value = "Disabled")]
        Disabled
    }
}