﻿using System;
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

            student = this.Students.
                          Include(er => er.EnrollmentRequests).
                          Include(l => l.Lessons).
                          Where(u => u.Email == email && u.Pass == pass).FirstOrDefault();

            if (student == null)
            {
                instructor = this.Instructors.
                          Include(i => i.EnrollmentRequests).
                          Include(i => i.Lessons).
                          Include(i => i.Students).
                          Where(u => u.Email == email && u.Pass == pass).FirstOrDefault();
            }
            else
                return student;

            if (instructor == null)
            {
                schoolManager = this.SchoolManagers.
                          Include(sm => sm.Instructors).
                          Where(u => u.Email == email && u.Pass == pass).FirstOrDefault();
            }
            else
                return instructor;
            
            
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
    }
}
