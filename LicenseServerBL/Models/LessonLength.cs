using System;
using System.Collections.Generic;

#nullable disable

namespace LicenseServerBL.Models
{
    public partial class LessonLength
    {
        public LessonLength()
        {
            Instructors = new HashSet<Instructor>();
            Students = new HashSet<Student>();
        }

        public int LessonLengthId { get; set; }
        public int Slength { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
