using System;
using System.Collections.Generic;

#nullable disable

namespace LicenseServerBL.Models
{
    public partial class DrivingSchool
    {
        public DrivingSchool()
        {
            Instructors = new HashSet<Instructor>();
            SchoolManagers = new HashSet<SchoolManager>();
        }

        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public int AreaId { get; set; }
        public int EstablishmentYear { get; set; }
        public int NumOfTeachers { get; set; }

        public virtual Area Area { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<SchoolManager> SchoolManagers { get; set; }
    }
}
