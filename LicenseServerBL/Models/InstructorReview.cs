using System;
using System.Collections.Generic;

#nullable disable

namespace LicenseServerBL.Models
{
    public partial class InstructorReview
    {
        public int ReviewId { get; set; }
        public int InstructorId { get; set; }

        public virtual Instructor Instructor { get; set; }
        public virtual Review Review { get; set; }
    }
}
