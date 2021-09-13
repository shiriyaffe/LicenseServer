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
    }
}
