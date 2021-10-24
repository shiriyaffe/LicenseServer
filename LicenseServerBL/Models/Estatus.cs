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
        }

        public int StatusId { get; set; }
        public string StatusMeaning { get; set; }

        public virtual ICollection<EnrollmentRequest> EnrollmentRequests { get; set; }
    }
}
