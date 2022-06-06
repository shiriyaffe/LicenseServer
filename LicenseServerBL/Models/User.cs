using System;
using System.Collections.Generic;

#nullable disable

namespace LicenseServerBL.Models
{
    public partial class User
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string UserPswd { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string UserImg { get; set; }
    }
}
