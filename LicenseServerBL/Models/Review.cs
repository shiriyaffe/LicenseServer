using System;
using System.Collections.Generic;

#nullable disable

namespace LicenseServerBL.Models
{
    public partial class Review
    {
        public Review()
        {
            InstructorReviews = new HashSet<InstructorReview>();
            Lessons = new HashSet<Lesson>();
            StudentSummaries = new HashSet<StudentSummary>();
        }

        public int ReviewId { get; set; }
        public string Content { get; set; }
        public DateTime WrittenOn { get; set; }

        public virtual ICollection<InstructorReview> InstructorReviews { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<StudentSummary> StudentSummaries { get; set; }
    }
}
