using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LicenseServerBL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LicenseServerBL.Models
{
    public partial class LicenseDBContext : DbContext
    {
        public Object Login(string email, string pass)
        {
            Object user = this.Students.
                          Include(er => er.EnrollmentRequests).
                          Include(l => l.Lessons).
                          Where(u => u.Email == email && u.Pass == pass).FirstOrDefault();

            if (user == null)
            {
                user = this.Instructors.
                          Include(i => i.EnrollmentRequests).
                          Include(i => i.Lessons).
                          Include(i => i.Students).
                          Where(u => u.Email == email && u.Pass == pass).FirstOrDefault();
            }
            else if (user == null)
            {
                user = this.SchoolManagers.
                          Include(sm => sm.Instructors).
                          Where(u => u.Email == email && u.Pass == pass).FirstOrDefault();
            }

            return user;
        }

        public void AddStudent(Student student)
        {
            try
            {
                this.Students.Add(student);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
