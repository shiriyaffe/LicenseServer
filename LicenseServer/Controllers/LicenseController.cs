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
                this.context.AddStudent(student);
                HttpContext.Session.SetObject("theUser", student);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                return student;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
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
                LicenseTypes = context.LicenseTypes.ToList()
            };
            return tables;
        }
    }
}

    

