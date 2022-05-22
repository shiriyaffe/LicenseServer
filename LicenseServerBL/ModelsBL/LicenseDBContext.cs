using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        const int APPROVED = 2;
        const int WAITING = 1;

        public Object Login(string email, string pass)
        {
            Student student = new Student();
            Instructor instructor = new Instructor();
            SchoolManager schoolManager = new SchoolManager();

            Student s = this.Students.
                          Include(er => er.EnrollmentRequests).
                          Include(l => l.Lessons).
                          Include(i => i.Instructor).
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
                foreach(Student s in this.Students)
                {
                    if (s.Email.Equals(student.Email))
                        return false;
                }

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
                foreach (Instructor i in this.Instructors)
                {
                    if (i.Email.Equals(instructor.Email))
                        return false;
                }

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
                foreach (SchoolManager s in this.SchoolManagers)
                {
                    if (s.Email.Equals(sManager.Email))
                        return false;
                }

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

        public bool AddSummary(StudentSummary summary)
        {
            try
            {
                this.StudentSummarys.Add(summary);
                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool AddInstructorReview(InstructorReview review)
        {
            try
            {
                this.InstructorReviews.Add(review);
                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool AddReview(Review review)
        {
            try
            {
                this.Reviews.Add(review);
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

        public ObservableCollection<Student> GetAllStudents()
        {
            ObservableCollection<Student> students = new ObservableCollection<Student>();
            foreach(Student s in this.Students.Include(p => p.Instructor))
            {
                students.Add(s);
            }
            return students;
        }

        public List<EnrollmentRequest> GetEnrollments()
        {
            return this.EnrollmentRequests.Include(em => em.Student).ToList();
        }

        private int GetManagerID(int schoolId)
        {
            SchoolManager manager = new SchoolManager();
            foreach(SchoolManager sm in this.SchoolManagers)
            {
                if (sm.SchoolId == schoolId)
                    return sm.SmanagerId;
            }
            return 0;
        }

        public bool ChangeStatusForUser(object u)
        {
            try
            {
                if (u is Student)
                {
                    Student s = (Student)u;
                    Student student = new Student();
                    student = this.Students.Where(st => st.StudentId == s.StudentId).FirstOrDefault();
                    student.EStatusId = s.EStatusId;
                    this.Students.Update(student);

                    EnrollmentRequest em = this.EnrollmentRequests.Where(e => e.StudentId != null && e.StudentId == student.StudentId).FirstOrDefault();
                    if (em != null)
                    {
                        em.StatusId = (int)student.EStatusId;
                        this.EnrollmentRequests.Update(em);

                        if(s.EStatusId == APPROVED)
                        {
                            student.InstructorId = em.InstructorId;
                            this.Students.Update(student);
                        }
                    }
                    else
                    { 
                    }
                    this.SaveChanges();

                    return true;
                }

                else if (u is Instructor)
                {
                    Instructor i = (Instructor)u;
                    Instructor teacher = new Instructor();

                    teacher = this.Instructors.Where(t => t.InstructorId == i.InstructorId).FirstOrDefault();
                    teacher.EStatusId = i.EStatusId;
                    if (i.EStatusId == APPROVED)
                    {
                        int managerId = GetManagerID(i.DrivingSchoolId);
                        teacher.SchoolManagerId = managerId;
                    }
                    this.Instructors.Update(teacher);

                    EnrollmentRequest em = this.EnrollmentRequests.Where(e => e.StudentId == null && e.InstructorId != null && e.InstructorId == teacher.InstructorId).FirstOrDefault();
                    em.StatusId = (int)teacher.EStatusId;
                    this.EnrollmentRequests.Update(em);
                    this.SaveChanges();

                    return true;
                }

                else { return false; }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public Lesson UpdateLessonSum(Lesson lesson, Lesson updatedLesson)
        {
            try
            {
                lesson.ReviewId = updatedLesson.ReviewId;

                this.SaveChanges();
                return lesson;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public Lesson CancelLesson(Lesson lesson, Lesson cancelLesson)
        {
            try
            {
                lesson.IsAvailable = cancelLesson.IsAvailable;
                lesson.IsPaid = cancelLesson.IsPaid;
                lesson.HasDone = cancelLesson.HasDone;
                lesson.StuudentId = cancelLesson.StuudentId;
                lesson.EStatusId = cancelLesson.EStatusId;
                lesson.ReviewId = cancelLesson.ReviewId;

                this.SaveChanges();
                return lesson;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public List<Lesson> GetLessons()
        {
            return this.Lessons.Include(l => l.Stuudent).ToList();
        }

        public bool CheckIfAvailable(Lesson l)
        {
            bool available = true;
            int count = 0;

            foreach(Lesson lesson in this.Lessons)
            {
                if(lesson.InstructorId == l.InstructorId)
                {
                    if(lesson.Ldate.CompareTo(l.Ldate) == 0 && lesson.Ltime.Equals(l.Ltime))
                    {
                        if (lesson.EStatusId != APPROVED && lesson.EStatusId != WAITING && count == 0)
                            available = true;
                        else
                        {
                            available = false;
                            count++;
                        }
                    }

                }
            }

            return available;
        }

        public bool AddNewLesson(Lesson lesson)
        {
            try
            {
                this.Lessons.Add(lesson);
                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public Lesson ApproveLesson(Lesson lesson, Lesson updatedLesson)
        {
            try
            {
                lesson.EStatusId = updatedLesson.EStatusId;

                this.SaveChanges();
                return lesson;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool CheckIfMailExists(string mail)
        {
            foreach(Student s in this.Students)
            {
                if (s.Email.Equals(mail))
                    return true;
            }
            foreach (Instructor i in this.Instructors)
            {
                if (i.Email.Equals(mail))
                    return true;
            }
            foreach (SchoolManager sm in this.SchoolManagers)
            {
                if (sm.Email.Equals(mail))
                    return true;
            }

            return false;
        }

        public bool CheckIfSumExists(int lessonId)
        {
            foreach(StudentSummary ss in this.StudentSummarys)
            {
                if (ss.LessonId == lessonId)
                    return true;
            }
            return false;
        }
    }
}
