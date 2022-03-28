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

        [Route("SayHello")]
        [HttpGet]
        public string SayHello()
        {
            return "Hello World";
        }

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

        [Route("SignUpStudent")]
        [HttpPost]
        public Student SignUpStudent([FromBody] Student student)
        {
            if(student != null)
            {
                bool signedUp = this.context.AddStudent(student);
                if (signedUp)
                {
                    HttpContext.Session.SetObject("theUser", student);

                    var pathFrom = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", "defaultPhoto.png");
                    var pathTo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", $"{student.StudentId}.jpg");
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

        [Route("GetAreaName")]
        [HttpGet]
        public string GetAreaName([FromQuery] int areaId)
        {
            foreach(Area a in context.Areas)
            {
                if (a.AreaId == areaId)
                    return a.AreaName;
            }
            return "";
        }

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
                    HttpContext.Session.SetObject("theUser", dSchool);
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
                    HttpContext.Session.SetObject("theUser", enrollment);
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
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


        [Route("GetInstructors")]
        [HttpGet]
        public ObservableCollection<Instructor> GetInstructors()
        {
            ObservableCollection<Instructor> instructors = new ObservableCollection<Instructor>();
            foreach(Instructor i in context.Instructors)
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
                if (l.StuudentId == studentId && l.HasDone)
                    lessons.Add(l);
            }

            return lessons;
        }

        [Route("GetStudentsByInstructor")]
        [HttpGet]
        public ObservableCollection<Student> GetStudentsByInstructor([FromQuery] int instructorId)
        {
            ObservableCollection<Student> students = new ObservableCollection<Student>();
            foreach (Student s in context.Students)
            {
                if (s.InstructorId == instructorId)
                    students.Add(s);
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
            User user = HttpContext.Session.GetObject<User>("theUser");
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
        public Instructor DeleteInstructor([FromQuery] int instructortId)
        {
            Instructor instructor = context.Instructors.Where(i => i.InstructorId == instructortId).FirstOrDefault();

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
                if(s.InstructorId != null && s.InstructorId > 0)
                {
                    foreach(Instructor i in context.Instructors)
                    {
                        if (i.InstructorId == s.InstructorId)
                        {
                            if (i.SchoolManagerId != null && i.SchoolManagerId != sManagerId)
                                students.Remove(s);
                        }
                    }
                }
            }

            return students;
        }
    }
}

    

