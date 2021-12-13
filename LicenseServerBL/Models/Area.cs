using System;
using System.Collections.Generic;

#nullable disable

namespace LicenseServerBL.Models
{
    public partial class Area
    {
        public Area()
        {
            Cities = new HashSet<City>();
            DrivingSchools = new HashSet<DrivingSchool>();
            Instructors = new HashSet<Instructor>();
        }

        public int AreaId { get; set; }
        public string AreaName { get; set; }

        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<DrivingSchool> DrivingSchools { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
    }
}
