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
            Instructors = new HashSet<Instructor>();
            SchoolManagers = new HashSet<SchoolManager>();
        }

        public int AreaId { get; set; }
        public string AreaName { get; set; }

        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<SchoolManager> SchoolManagers { get; set; }
    }
}
