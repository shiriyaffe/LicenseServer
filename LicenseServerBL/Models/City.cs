using System;
using System.Collections.Generic;

#nullable disable

namespace LicenseServerBL.Models
{
    public partial class City
    {
        public City()
        {
            Students = new HashSet<Student>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public int? AreaId { get; set; }

        public virtual Area Area { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
