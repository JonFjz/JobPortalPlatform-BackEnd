using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
