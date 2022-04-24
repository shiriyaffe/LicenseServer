using System;
using System.Collections.Generic;

#nullable disable

namespace LicenseServerBL.Models
{
    public partial class Estatus
    {
        public Estatus()
        {
            EnrollmentRequests = new HashSet<EnrollmentRequest>();
            Instructors = new HashSet<Instructor>();
            Lessons = new HashSet<Lesson>();
            SchoolManagers = new HashSet<SchoolManager>();
            Students = new HashSet<Student>();
        }

        public int StatusId { get; set; }
        public string StatusMeaning { get; set; }

        public virtual ICollection<EnrollmentRequest> EnrollmentRequests { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<SchoolManager> SchoolManagers { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
