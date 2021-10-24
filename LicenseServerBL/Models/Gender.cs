using System;
using System.Collections.Generic;

#nullable disable

namespace LicenseServerBL.Models
{
    public partial class Gender
    {
        public Gender()
        {
            Instructors = new HashSet<Instructor>();
            SchoolManagers = new HashSet<SchoolManager>();
            Students = new HashSet<Student>();
        }

        public int GenderId { get; set; }
        public string GenderType { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<SchoolManager> SchoolManagers { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
