using System;
using System.Collections.Generic;

#nullable disable

namespace LicenseServerBL.Models
{
    public partial class AppAdmin
    {
        public int AdminId { get; set; }
        public string Aemail { get; set; }
        public string Apass { get; set; }
    }
}
