﻿using System;
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
        public string DrivingSchool { get; set; }
        public int AreaId { get; set; }
        public int EstablishmentYear { get; set; }
        public int NumOfTeachers { get; set; }
        public DateTime RegistrationDate { get; set; }

        public virtual Area Area { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
    }
}
