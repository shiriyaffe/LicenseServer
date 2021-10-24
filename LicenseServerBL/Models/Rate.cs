using System;
using System.Collections.Generic;

#nullable disable

namespace LicenseServerBL.Models
{
    public partial class Rate
    {
        public Rate()
        {
            Instructors = new HashSet<Instructor>();
        }

        public int RateId { get; set; }
        public string RateMeaning { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; }
    }
}
