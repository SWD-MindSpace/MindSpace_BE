using System.Runtime.Serialization;

namespace MindSpace.Domain.Entities.Constants
{
    public enum UserStatus
    {
        [EnumMember(Value = "Enabled")]
        Enabled,

        [EnumMember(Value = "Disabled")]
        Disabled
    }
}
