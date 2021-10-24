using System;
using System.Collections.Generic;

#nullable disable

namespace LicenseServerBL.Models
{
    public partial class Review
    {
        public Review()
        {
            Lessons = new HashSet<Lesson>();
        }

        public int ReviewId { get; set; }
        public string Content { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
