using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicenseServerBL.Models;

namespace LicenseServer.DTO
{
    public class LookupTables
    {
        public List<City> Cities { get; set; }
        public List<Area> Areas { get; set; }
        public List<Gearbox> GearBoxes { get; set; }
        public List<Gender> Genders { get; set; }
        public List<LicenseType> LicenseTypes { get; set; }
    }
}
