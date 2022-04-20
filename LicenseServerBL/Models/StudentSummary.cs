using System;
using System.Collections.Generic;

#nullable disable

namespace LicenseServerBL.Models
{
    public partial class StudentSummary
    {
        public int ReviewId { get; set; }
        public int StudentId { get; set; }

        public virtual Review Review { get; set; }
        public virtual Student Student { get; set; }
    }
}
