using System;
using System.Collections.Generic;

#nullable disable

namespace LicenseServerBL.Models
{
    public partial class SchoolManager
    {
        public SchoolManager()
        {
            Instructors = new HashSet<Instructor>();
        }

        public int SmanagerId { get; set; }
        public string Smname { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string PhoneNumber { get; set; }
        public int GenderId { get; set; }
        public DateTime? Birthday { get; set; }
        public int? SchoolId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int EStatusId { get; set; }

        public virtual Estatus EStatus { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual DrivingSchool School { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
    }
}
