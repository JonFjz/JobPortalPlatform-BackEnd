using System.Runtime.Serialization;

namespace JobPortal.Domain.Enums
{
    public enum PaymentStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "Completed")]
        Completed,

        [EnumMember(Value = "Failed")]
        Failed,
    }
}
