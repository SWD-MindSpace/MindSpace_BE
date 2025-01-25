using System.Runtime.Serialization;

namespace MindSpace.Domain.Commons.Constants
{
    public enum UserStatus
    {
        [EnumMember(Value = "Enabled")]
        Enabled,

        [EnumMember(Value ="Disabled")]
        Disabled
    }
}
