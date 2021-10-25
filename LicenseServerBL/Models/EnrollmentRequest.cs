using System;
using System.Collections.Generic;

#nullable disable

namespace LicenseServerBL.Models
{
    public partial class EnrollmentRequest
    {
        public int EnrollmentId { get; set; }
        public int LessonId { get; set; }
        public int StatusId { get; set; }
        public int StudentId { get; set; }
        public int InstructorId { get; set; }

        public virtual Instructor Instructor { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual Estatus Status { get; set; }
        public virtual Student Student { get; set; }
    }
}
