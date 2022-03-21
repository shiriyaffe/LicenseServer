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
            Student student = new Student();
            Instructor instructor = new Instructor();
            SchoolManager schoolManager = new SchoolManager();

            Student s = this.Students.
                          Include(er => er.EnrollmentRequests).
                          Include(l => l.Lessons).
                          Where(u => u.Email == email && u.Pass == pass).FirstOrDefault();

            if (s == null)
            {
                Instructor i = this.Instructors.
                          Include(i => i.EnrollmentRequests).
                          Include(i => i.Lessons).
                          Include(i => i.Students).
                          Where(u => u.Email == email && u.Pass == pass).FirstOrDefault();
                instructor = i;
            }
            else
            {
                student = s;
                return student;
            }

            if (instructor == null)
            {
                schoolManager = this.SchoolManagers.
                          Include(sm => sm.Instructors).
                          Where(u => u.Email == email && u.Pass == pass).FirstOrDefault();
            }
            else
            {
                return instructor;
            }
            
            return schoolManager;
        }

        public bool IsStudentDB(Object student)
        {
            foreach(Student s in Students)
            {
                if (s.Email == ((Student)student).Email && s.Pass == ((Student)student).Pass)
                    return true;
            }
                
            return false;
        }

        public bool AddStudent(Student student)
        {
            try
            {
                this.Students.Add(student);
                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool AddInstructor(Instructor instructor)
        {
            try
            {
                this.Instructors.Add(instructor);
                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool AddSManager(SchoolManager sManager)
        {
            try
            {
                this.SchoolManagers.Add(sManager);
                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool AddDSchool(DrivingSchool dSchool)
        {
            try
            {
                this.DrivingSchools.Add(dSchool);
                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool AddEnrollment(EnrollmentRequest enrollment)
        {
            try
            {
                this.EnrollmentRequests.Add(enrollment);
                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public Student UpdateStudent(Student student, Student updatedStudent)
        {
            try
            {
                Student currentUser = this.Students
                .Where(u => u.StudentId == student.StudentId).FirstOrDefault();

                currentUser.Pass = updatedStudent.Pass;
                currentUser.PhoneNumber = updatedStudent.PhoneNumber;
                currentUser.CityId = updatedStudent.CityId;
                currentUser.GearboxId = updatedStudent.GearboxId;
                currentUser.LessonLengthId = updatedStudent.LessonLengthId;
                currentUser.Saddress = updatedStudent.Saddress;


                this.SaveChanges();
                return currentUser;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public Student DeleteStudentFromInstructor(Student student)
        {
            try
            {
                student.InstructorId = null;

                this.SaveChanges();
                return student;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public Instructor DeleteInstructorFromSManager(Instructor instructor)
        {
            try
            {
                instructor.SchoolManagerId = null;
                instructor.DrivingSchoolId = 0;

                this.SaveChanges();
                return instructor;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public SchoolManager UpdateSchoolManager(SchoolManager schoolManager, SchoolManager updatedSchoolManager)
        {
            try
            {
                SchoolManager currentUser = this.SchoolManagers
                .Where(u => u.SmanagerId == schoolManager.SmanagerId).FirstOrDefault();

                currentUser.Pass = updatedSchoolManager.Pass;
                currentUser.PhoneNumber = updatedSchoolManager.PhoneNumber;


                this.SaveChanges();
                return currentUser;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public Instructor UpdateInstructor(Instructor instructor, Instructor updatedInstructor)
        {
            try
            {
                Instructor currentUser = this.Instructors
                .Where(u => u.InstructorId == instructor.InstructorId).FirstOrDefault();

                currentUser.Pass = updatedInstructor.Pass;
                currentUser.Birthday = updatedInstructor.Birthday;
                currentUser.PhoneNumber = updatedInstructor.PhoneNumber;
                currentUser.AreaId = updatedInstructor.AreaId;
                currentUser.GearboxId = updatedInstructor.GearboxId;
                currentUser.LessonLengthId = updatedInstructor.LessonLengthId;
                currentUser.Price = updatedInstructor.Price;
                currentUser.Details = updatedInstructor.Details;


                this.SaveChanges();
                return currentUser;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
