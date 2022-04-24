using System;
using System.Collections.Generic;

#nullable disable

namespace LicenseServerBL.Models
{
    public partial class Lesson
    {
        public Lesson()
        {
            EnrollmentRequests = new HashSet<EnrollmentRequest>();
        }

        public int LessonId { get; set; }
        public DateTime Ldate { get; set; }
        public string Lday { get; set; }
        public bool IsAvailable { get; set; }
        public int? StuudentId { get; set; }
        public bool IsPaid { get; set; }
        public bool HasDone { get; set; }
        public int InstructorId { get; set; }
        public int? ReviewId { get; set; }
        public int EStatusId { get; set; }
        public string Ltime { get; set; }

        public virtual Estatus EStatus { get; set; }
        public virtual Instructor Instructor { get; set; }
        public virtual Review Review { get; set; }
        public virtual Student Stuudent { get; set; }
        public virtual ICollection<EnrollmentRequest> EnrollmentRequests { get; set; }
    }
}
