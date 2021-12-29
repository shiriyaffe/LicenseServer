using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicenseServerBL.Models;

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

        
        [Route("GetInstructors")]
        [HttpGet]
        public List<Instructor> GetInstructors()
        {
            List<Instructor> instractors = new List<Instructor>();
            foreach(Instructor i in context.Instructors)
            {
                instractors.Add(i);
            }
            
            return instractors;
        }

        [Route("GetLookups")]
        [HttpGet]
        public DTO.LookupTables GetLookups()
        {
            DTO.LookupTables tables = new DTO.LookupTables()
            {
                Cities = context.Cities.ToList(),
                Areas = context.Areas.ToList(),
                GearBoxes = context.Gearboxes.ToList(),
                Genders = context.Genders.ToList(),
                LicenseTypes = context.LicenseTypes.ToList(),
                LessonLengths = context.LessonLengths.ToList(),
                DrivingSchools = context.DrivingSchools.ToList(),
                WorkingHours = context.WorkingHours.ToList()
            };
            return tables;
        }
    }
}

    

