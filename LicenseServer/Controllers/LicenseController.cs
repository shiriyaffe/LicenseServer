using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicenseServerBL.Models;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Net.Mail;
using LicenseServer.Helper;

namespace LicenseServer.Controllers
{
    [Route("LicenseAPI")]
    [ApiController]
    public class LicenseController : ControllerBase
    {
        LicenseDBContext context;
        public LicenseController(LicenseDBContext context)
        {
            this.context = context;
        }

        private const int WAITING_STATUS = 1;
        private const int APPROVED_STATUS = 2;
        private const int DENIED_STATUS = 3;

        [Route("Login")]
        [HttpGet]
        public Object Login([FromQuery] string email, [FromQuery] string pass)
        {
            Object user = context.Login(email, pass);

            //Check user name and password
            if (user != null)
            {
                HttpContext.Session.SetObject("theUser", user);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                return user;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("Logout")]
        [HttpPost]
        public bool Logout([FromBody] Object user)
        {
            try
            {
                Object current = HttpContext.Session.GetObject<Object>("theUser");

                if (current != null)
                {
                    HttpContext.Session.Remove("theUser");
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return true;
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return false;
                }
            }
            catch
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return false;
            }
        }

        [Route("SignUpStudent")]
        [HttpPost]
        public Student SignUpStudent([FromBody] Student student)
        {
            if (student != null)
            {
                bool signedUp = this.context.AddStudent(student);
                if (signedUp)
                {
                    HttpContext.Session.SetObject("theUser", student);

                    var pathFrom = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", "defaultPhoto.png");
                    var pathTo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Students", $"{student.StudentId}.jpg");
                    System.IO.File.Copy(pathFrom, pathTo);

                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                    return student;
                }
                else
                    return null;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("UpdateStudent")]
        [HttpPost]
        public Student UpdateStudent([FromBody] Student student)
        {
            //If user is null the request is bad
            if (student == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return null;
            }

            Student currentUser = HttpContext.Session.GetObject<Student>("theUser");
            //Check if user logged in and its ID is the same as the contact user ID
            if (currentUser != null && currentUser.InstructorId == student.InstructorId)
            {
                Student updatedStudent = context.UpdateStudent(currentUser, student);

                if (updatedStudent == null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return null;
                }

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return updatedStudent;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("UpdateSManager")]
        [HttpPost]
        public SchoolManager UpdateSManager([FromBody] SchoolManager schoolManager)
        {
            //If user is null the request is bad
            if (schoolManager == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return null;
            }

            SchoolManager currentUser = HttpContext.Session.GetObject<SchoolManager>("theUser");
            //Check if user logged in and its ID is the same as the contact user ID
            if (currentUser != null && currentUser.SmanagerId == schoolManager.SmanagerId)
            {
                SchoolManager updatedSchoolManager = context.UpdateSchoolManager(currentUser, schoolManager);

                if (updatedSchoolManager == null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return null;
                }

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return updatedSchoolManager;

                ////Now check if an image exist for the contact (photo). If not, set the default image!
                //var sourcePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", DEFAULT_PHOTO);
                //var targetPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", $"{user.Id}.jpg");
                //System.IO.File.Copy(sourcePath, targetPath);

                //return the contact with its new ID if that was a new contact
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("SignUpInstructor")]
        [HttpPost]
        public Instructor SignUpInstructor([FromBody] Instructor instructor)
        {
            if (instructor != null)
            {
                bool signedUp = this.context.AddInstructor(instructor);
                if (signedUp)
                {
                    HttpContext.Session.SetObject("theUser", instructor);
                    var pathFrom = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", "defaultPhoto.png");
                    var pathTo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Instructors", $"{instructor.InstructorId}.jpg");
                    System.IO.File.Copy(pathFrom, pathTo);
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                    return instructor;
                }
                else
                    return null;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("UpdateInstructor")]
        [HttpPost]
        public Instructor UpdateInstructor([FromBody] Instructor instructor)
        {
            //If user is null the request is bad
            if (instructor == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return null;
            }

            Instructor currentUser = HttpContext.Session.GetObject<Instructor>("theUser");
            //Check if user logged in and its ID is the same as the contact user ID
            if (currentUser != null && currentUser.InstructorId == instructor.InstructorId)
            {
                Instructor updatedInstructor = context.UpdateInstructor(currentUser, instructor);

                if (updatedInstructor == null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return null;
                }

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return updatedInstructor;

                ////Now check if an image exist for the contact (photo). If not, set the default image!
                //var sourcePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", DEFAULT_PHOTO);
                //var targetPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", $"{user.Id}.jpg");
                //System.IO.File.Copy(sourcePath, targetPath);

                //return the contact with its new ID if that was a new contact
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        //[Route("GetAreaName")]
        //[HttpGet]
        //public string GetAreaName([FromQuery] int areaId)
        //{
        //    foreach (Area a in context.Areas)
        //    {
        //        if (a.AreaId == areaId)
        //            return a.AreaName;
        //    }
        //    return "";
        //}

        [Route("GetCityById")]
        [HttpGet]
        public City GetCityById([FromQuery] int cityId)
        {
            foreach (City c in context.Cities)
            {
                if (c.CityId == cityId)
                    return c;
            }
            return null;
        }

        [Route("GetAreaById")]
        [HttpGet]
        public Area GetAreaById([FromQuery] int areaId)
        {
            foreach (Area a in context.Areas)
            {
                if (a.AreaId == areaId)
                    return a;
            }
            return null;
        }

        [Route("GetHour")]
        [HttpGet]
        public WorkingHour GetHour([FromQuery] string wHour)
        {
            foreach (WorkingHour wh in context.WorkingHours)
            {
                if (wh.Whour.Equals(wHour))
                    return wh;
            }
            return null;
        }

        [Route("GetGearboxById")]
        [HttpGet]
        public Gearbox GetGearboxById([FromQuery] int gearboxId)
        {
            foreach (Gearbox g in context.Gearboxes)
            {
                if (g.GearboxId == gearboxId)
                    return g;
            }
            return null;
        }

        [Route("GetLessonLengthById")]
        [HttpGet]
        public LessonLength GetLessonLengthById([FromQuery] int lessonLenghId)
        {
            foreach (LessonLength l in context.LessonLengths)
            {
                if (l.LessonLengthId == lessonLenghId)
                    return l;
            }
            return null;
        }

        [Route("SignUpSchoolManager")]
        [HttpPost]
        public SchoolManager SignUpSManager([FromBody] SchoolManager sManager)
        {
            if (sManager != null)
            {
                bool signedUp = this.context.AddSManager(sManager);
                if (signedUp)
                {
                    HttpContext.Session.SetObject("theUser", sManager);

                    var pathFrom = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", "defaultPhoto.png");
                    var pathTo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/SchoolManagers", $"{sManager.SmanagerId}.jpg");
                    System.IO.File.Copy(pathFrom, pathTo);
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                    return sManager;
                }
                else
                    return null;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("AddDrivingSchool")]
        [HttpPost]
        public DrivingSchool AddNewDrivingSchool([FromBody] DrivingSchool dSchool)
        {
            if (dSchool != null)
            {
                bool added = this.context.AddDSchool(dSchool);
                if (added)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                    return dSchool;
                }
                else
                    return null;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }


        [Route("AddEnrollmentRequest")]
        [HttpPost]
        public EnrollmentRequest AddEnrollmentRequest([FromBody] EnrollmentRequest enrollment)
        {
            if (enrollment != null)
            {
                bool added = this.context.AddEnrollment(enrollment);
                if (added)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                    if(enrollment.LessonId == null)
                    {
                        if(enrollment.StudentId == null)
                        {
                            string smEmail = "";
                            string smName = "";

                            foreach(SchoolManager sm in context.SchoolManagers)
                            {
                                if (sm.SchoolId == enrollment.SchoolId)
                                {
                                    smEmail = sm.Email;
                                    smName = sm.Smname;
                                }
                            }
                            
                            EmailSender.SendEmail("בקשת רישום חדשה", "מורה חדש מבקש להירשם לבית הספר לנהיגה שלך! מהר להיכנס לאפליקציה על מנת לאשר או לדחות את הבקשה", $"{smEmail}", $"{smName}", "<easy2drive2022@gmail.com>", "easyDrive", "easyDrive2022", "smtp.gmail.com");
                        }
                        else if (enrollment.SchoolId == null)
                        {
                            string iEmail = "";
                            string iName = "";

                            foreach (Instructor i in context.Instructors)
                            {
                                if (i.InstructorId == enrollment.InstructorId)
                                {
                                    iEmail = i.Email;
                                    iName = i.Iname;
                                }
                            }

                            EmailSender.SendEmail("בקשת רישום חדשה", "תלמיד חדש מבקש להירשם אצלך! מהר להיכנס לאפליקציה על מנת לאשר או לדחות את הבקשה", $"{iEmail}", $"{iName}", "<easy2drive2022@gmail.com>", "easyDrive", "easyDrive2022", "smtp.gmail.com");
                        }
                    }

                    //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                    return enrollment;
                }
                else
                    return null;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("AddSummary")]
        [HttpPost]
        public StudentSummary AddSummary([FromBody] StudentSummary summary)
        {
            if (summary != null)
            {
                bool added = this.context.AddSummary(summary);
                if (added)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                    return summary;
                }
                else
                    return null;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("AddInstructorReview")]
        [HttpPost]
        public InstructorReview AddInstructorReview([FromBody] InstructorReview review)
        {
            if (review != null)
            {
                bool added = this.context.AddInstructorReview(review);
                if (added)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                    return review;
                }
                else
                    return null;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("AddReview")]
        [HttpPost]
        public Review AddReview([FromBody] Review review)
        {
            if (review != null)
            {
                bool added = this.context.AddReview(review);
                if (added)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                    return review;
                }
                else
                    return null;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }


        [Route("GetInstructors")]
        [HttpGet]
        public ObservableCollection<Instructor> GetInstructors()
        {
            ObservableCollection<Instructor> instructors = new ObservableCollection<Instructor>();
            foreach (Instructor i in context.Instructors)
            {
                instructors.Add(i);
            }

            return instructors;
        }

        [Route("GetLessons")]
        [HttpGet]
        public ObservableCollection<Lesson> GetLessons([FromQuery] int studentId)
        {
            ObservableCollection<Lesson> lessons = new ObservableCollection<Lesson>();
            foreach (Lesson l in context.Lessons)
            {
                if (l.StuudentId == studentId)
                    lessons.Add(l);
            }

            return lessons;
        }

        [Route("GetInstructorLessons")]
        [HttpGet]
        public ObservableCollection<Lesson> GetInstructorLessons([FromQuery] int instructorId)
        {
            ObservableCollection<Lesson> lessons = new ObservableCollection<Lesson>();
            List<Lesson> list = context.GetLessons();
            foreach (Lesson l in list)
            {
                if (l.InstructorId == instructorId)
                    lessons.Add(l);
            }

            return lessons;
        }

        [Route("GetInstructorReviews")]
        [HttpGet]
        public ObservableCollection<Review> GetReviews([FromQuery] int instructorID)
        {
            ObservableCollection<Review> reviews = new ObservableCollection<Review>();
            List<InstructorReview> list = context.InstructorReviews.ToList();
            foreach (InstructorReview l in list)
            {
                if(l.InstructorId == instructorID)
                {
                    Review rev = context.Reviews.Where(r => r.ReviewId == l.ReviewId).FirstOrDefault();
                    reviews.Add(rev);
                }
            }

            return reviews;
        }

        [Route("GetStudentsByInstructor")]
        [HttpGet]
        public ObservableCollection<Student> GetStudentsByInstructor([FromQuery] int instructorId)
        {
            ObservableCollection<Student> students = new ObservableCollection<Student>();
            ObservableCollection<Student> students1 = new ObservableCollection<Student>();

            students1 = this.context.GetAllStudents();

            foreach (Student s in students1)
            {
                if (s.InstructorId == instructorId)
                    students.Add(s);
            }

            return students;
        }

        [Route("GetWaitingStudentsByInstructor")]
        [HttpGet]
        public ObservableCollection<Student> GetWaitingStudentsByInstructor([FromQuery] int instructorId)
        {
            ObservableCollection<Student> students = new ObservableCollection<Student>();
            List<EnrollmentRequest> enrollmentRequests = context.GetEnrollments();
            foreach (EnrollmentRequest em in enrollmentRequests)
            {
                if (em.InstructorId == instructorId && em.StudentId != null && em.StatusId == WAITING_STATUS)
                    students.Add(em.Student);
            }

            return students;
        }

        [Route("GetLookups")]
        [HttpGet]
        public DTO.LookupTables GetLookups()
        {
            DTO.LookupTables tables = new DTO.LookupTables()
            {
                DrivingSchools = context.DrivingSchools.ToList(),
                Areas = context.Areas.ToList(),
                Cities = context.Cities.ToList(),
                GearBoxes = context.Gearboxes.ToList(),
                Genders = context.Genders.ToList(),
                LicenseTypes = context.LicenseTypes.ToList(),
                LessonLengths = context.LessonLengths.ToList(),
                Status = context.Estatuses.ToList(),
                WorkingHours = context.WorkingHours.ToList()
            };

            return tables;
        }

        [Route("IsStudent")]
        [HttpGet]
        public bool IsStudent([FromBody] Object s)
        {
            return context.IsStudentDB(s);
        }

        [Route("UploadImage")]
        [HttpPost]

        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            Object user = HttpContext.Session.GetObject<Object>("theUser");
            //Check if user logged in and its ID is the same as the contact user ID
            if (user != null)
            {
                if (file == null)
                {
                    return BadRequest();
                }

                try
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }


                    return Ok(new { length = file.Length, name = file.FileName });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return BadRequest();
                }
            }
            return Forbid();
        }

        [Route("DeleteStudent")]
        [HttpGet]
        public Student DeleteStudent([FromQuery] int studentId)
        {
            Student student = context.Students.Where(s => s.StudentId == studentId).FirstOrDefault();

            //If user is null the request is bad
            if (student == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return null;
            }
            else
            {
                Student deletedStudent = context.DeleteStudentFromInstructor(student);
                if (deletedStudent == null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return null;
                }

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return deletedStudent;
            }
        }

        [Route("DeleteInstructor")]
        [HttpGet]
        public Instructor DeleteInstructor([FromQuery] int instructorId)
        {
            Instructor instructor = context.Instructors.Where(i => i.InstructorId == instructorId).FirstOrDefault();

            //If user is null the request is bad
            if (instructor == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return null;
            }
            else
            {
                Instructor deletedInstructor = context.DeleteInstructorFromSManager(instructor);
                if (deletedInstructor == null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return null;
                }

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return deletedInstructor;
            }
        }

        [Route("GetStudentsBySchool")]
        [HttpGet]
        public ObservableCollection<Student> GetStudentsBySchool(int sManagerId)
        {
            ObservableCollection<Student> students = this.context.GetAllStudents();
            List<Student> students1 = context.Students.ToList<Student>();
            foreach (Student s in students1)
            {
                if (s.InstructorId != null && s.InstructorId > 0)
                {
                    foreach (Instructor i in context.Instructors)
                    {
                        if (i.InstructorId == s.InstructorId)
                        {
                            if (i.SchoolManagerId != null && i.SchoolManagerId != sManagerId)
                                students.Remove(s);
                        }
                    }
                }
                else
                    students.Remove(s);
            }

            return students;
        }

        [Route("ChangeInstructorStatus")]
        [HttpPost]
        public bool ChangeInstructorStatus(Instructor i)
        {
            try
            {
                Object user = HttpContext.Session.GetObject<Object>("theUser");

                if (user != null)
                {
                    bool ok = this.context.ChangeStatusForUser(i);

                    if (ok)
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                        EmailSender.SendEmail("סטטוס ההרשמה שלך עודכן!", "סטטוס ההרשמה שלך לבית הספר לנהיגה שבחרת עודכן באפליקציה! מהר להיכנס ולהתעדכן במצבך", $"{i.Email}", $"{i.Iname}", "<easy2drive2022@gmail.com>", "easyDrive", "easyDrive2022", "smtp.gmail.com");
                        return true;
                    }

                    else
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.NotModified;
                        return false;
                    }
                }

                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return false;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        [Route("ChangeStudentStatus")]
        [HttpPost]
        public bool ChangeStudentStatus(Student s)
        {
            try
            {
                Object user = HttpContext.Session.GetObject<Object>("theUser");

                if (user != null)
                {
                    bool ok = this.context.ChangeStatusForUser(s);

                    if (ok)
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                        EnrollmentRequest em = context.EnrollmentRequests.Where(e => e.StudentId != null && e.StudentId == s.StudentId).FirstOrDefault();
                        if (em != null)
                        {
                            if(em.StatusId == APPROVED_STATUS)
                                EmailSender.SendEmail("סטטוס ההרשמה שלך עודכן!", "בקשתך לרישום למורה אושרה בהצלחה. כנס לאפליקציה וקבע שיעור ראשון\nבהצלחה :)", $"{s.Email}", $"{s.Sname}", "<easy2drive2022@gmail.com>", "easyDrive", "easyDrive2022", "smtp.gmail.com");
                            if(em.StatusId == DENIED_STATUS)
                                EmailSender.SendEmail("סטטוס ההרשמה שלך עודכן!", "לצערנו בקשתך לרישום למורה נדחתה. כנס לאפליקציה ושלח בקשה נוספת..", $"{s.Email}", $"{s.Sname}", "<easy2drive2022@gmail.com>", "easyDrive", "easyDrive2022", "smtp.gmail.com");
                        }

                        return true;
                    }
                    else
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.NotModified;
                        return false;
                    }
                }

                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        [Route("UpdateLessonSum")]
        [HttpPost]
        public Lesson UpdateLessonSum([FromBody] Lesson lesson)
        {
            //If user is null the request is bad
            if (lesson == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return null;
            }

            Lesson current = context.Lessons.Where(l => l.LessonId == lesson.LessonId).FirstOrDefault();
            if(current != null)
            {
                Lesson updatedLesson = context.UpdateLessonSum(current, lesson);

                if (updatedLesson == null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return null;
                }

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return updatedLesson;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("GetInstructorById")]
        [HttpGet]
        public Instructor GetInstructorById([FromQuery] int instructorId)
        {
            foreach (Instructor i in context.Instructors)
            {
                if (i.InstructorId == instructorId)
                    return i;
            }
            return null;
        }

        [Route("CancelLesson")]
        [HttpPost]
        public Lesson CancelLesson([FromBody] Lesson lesson)
        {
            //If user is null the request is bad
            if (lesson == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return null;
            }

            Lesson current = context.Lessons.Where(l => l.LessonId == lesson.LessonId).FirstOrDefault();

            int formerStatus = current.EStatusId;

            Student student = new Student();
            foreach (Student s in context.Students)
            {
                if (s.StudentId == current.StuudentId)
                    student = s;
            }

            //Check if user logged in and its ID is the same as the contact user ID
            if (current != null)
            {
                Lesson cancelled = context.CancelLesson(current, lesson);

                if (cancelled == null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return null;
                }

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                if(formerStatus == APPROVED_STATUS)
                    EmailSender.SendEmail("עדכון שיעור", $"השיעור שלך בתאריך {cancelled.Ldate} בוטל על ידי המורה. מהר להיכנס לאפליקציה ולקבוע שיעור חדש", $"{student.Email}", $"{student.Sname}", "<easy2drive2022@gmail.com>", "easyDrive", "easyDrive2022", "smtp.gmail.com");
                else if(formerStatus == WAITING_STATUS)
                    EmailSender.SendEmail("עדכון שיעור", $"בקשתך לקבוע שיעור בתאריך {cancelled.Ldate} ובשעה {cancelled.Ltime} נדחתה על ידי המורה. כנס לאפליקציה ושלח בקשה לקביעת שיעור נוסף", $"{student.Email}", $"{student.Sname}", "<easy2drive2022@gmail.com>", "easyDrive", "easyDrive2022", "smtp.gmail.com");


                return cancelled;

                ////Now check if an image exist for the contact (photo). If not, set the default image!
                //var sourcePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", DEFAULT_PHOTO);
                //var targetPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", $"{user.Id}.jpg");
                //System.IO.File.Copy(sourcePath, targetPath);

                //return the contact with its new ID if that was a new contact
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("GetNewStudents")]
        [HttpGet]
        public List<Student> GetNewStudents([FromQuery] int num)
        {
            List<Student> students = new List<Student>();
            List<Student> newStudents = new List<Student>();

            students = context.GetAllStudents().ToList();

            foreach (Student s in students)
            {
                if (num == 1)
                {
                    if (s.RegistrationDate.Month == DateTime.Today.Month)
                        newStudents.Add(s);
                }
                else if(num == 2)
                {
                    if (s.RegistrationDate.Month == DateTime.Today.Month)
                    {
                        if ((DateTime.Today.AddDays(-1 * s.RegistrationDate.Day)).Day <= 7)
                            newStudents.Add(s);
                    }
                }
                else if(num == 3)
                {
                    DateTime today = DateTime.Today;
                    DateTime student = s.RegistrationDate;
                    if (today.Day == student.Day && today.Month == student.Month && today.Year == student.Year)
                        newStudents.Add(s);
                }
            }

            return newStudents;
        }

        [Route("CheckIfAvailable")]
        [HttpPost]
        public bool CheckIfAvailable([FromBody] Lesson l)
        {
            try
            {
                if (l != null)
                {
                    bool ok = this.context.CheckIfAvailable(l);

                    if (ok)
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                        return true;
                    }
                    else
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.NotModified;
                        return false;
                    }
                }

                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        [Route("AddNewLesson")]
        [HttpPost]
        public Lesson AddNewLesson([FromBody] Lesson lesson)
        {
            if (lesson != null)
            {
                bool added = this.context.AddNewLesson(lesson);
                if (added)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                    return lesson;
                }
                else
                    return null;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("ApproveLesson")]
        [HttpPost]
        public Lesson ApproveLesson([FromBody] Lesson lesson)
        {
            //If user is null the request is bad
            if (lesson == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return null;
            }

            Lesson current = context.Lessons.Where(l => l.LessonId == lesson.LessonId && l.EStatusId == WAITING_STATUS).FirstOrDefault();
            if (current != null)
            {
                foreach (Lesson lesson1 in context.Lessons)
                {
                    if (lesson1.InstructorId == current.InstructorId)
                    {
                        if (lesson1.Ltime.Equals(current.Ltime) && lesson1.Ldate.CompareTo(current.Ldate) == 0)
                        {
                            if (lesson1.EStatusId == APPROVED_STATUS)
                                return new Lesson();
                        }
                    }
                }

                Lesson updatedLesson = context.ApproveLesson(current, lesson);

                if (updatedLesson == null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return null;
                }

                Student student = new Student();
                foreach (Student s in context.Students)
                {
                    if (s.StudentId == current.StuudentId)
                        student = s;
                }

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                EmailSender.SendEmail("עדכון שיעור", $"בקשתך לשיעור בתאריך {updatedLesson.Ldate} ובשעה {updatedLesson.Ltime} אושרה בהצלחה!", $"{student.Email}", $"{student.Sname}", "<easy2drive2022@gmail.com>", "easyDrive", "easyDrive2022", "smtp.gmail.com");

                return updatedLesson;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("CheckIfMailExists")]
        [HttpGet]
        public bool CheckIfMailExists([FromQuery] string mail)
        {
            try
            {
                if (mail != "")
                {
                    bool exist = this.context.CheckIfMailExists(mail);

                    if (exist)
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                        return true;
                    }
                    else
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.NotModified;
                        return false;
                    }
                }

                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        [Route("GetLessonSum")]
        [HttpGet]
        public string GetLessonSum([FromQuery] int reviewId)
        {
            foreach(Review r in context.Reviews)
            {
                if(r.ReviewId == reviewId)
                {
                    return r.Content;
                }
            }

            return null;
        }

        [Route("CheckIfSumExists")]
        [HttpGet]
        public bool CheckIfSumExists([FromQuery] int lessonId)
        {
            try
            {
                if (lessonId > 0)
                {
                    bool exist = this.context.CheckIfSumExists(lessonId);

                    if (exist)
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                        return true;
                    }
                    else
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.NotModified;
                        return false;
                    }
                }

                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        [Route("ChangeRating")]
        [HttpPost]
        public bool ChangeRating(Instructor i)
        {
            try
            {
                Object user = HttpContext.Session.GetObject<Object>("theUser");

                if (user != null)
                {
                    bool ok = this.context.ChangeRating(i);

                    if (ok)
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                        return true;
                    }

                    else
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.NotModified;
                        return false;
                    }
                }

                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        [Route("SetLessonsCount")]
        [HttpGet]
        public bool SetLessonsCount()
        {
             return this.context.SetLessonsCount();
        }

        [Route("SetPastLessons")]
        [HttpGet]
        public bool SetPastLessons()
        {
            return this.context.SetPastLessons();
        }
    }
}

    

