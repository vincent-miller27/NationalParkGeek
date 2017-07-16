using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Survey
    {
        public string ParkCode { get; set; }
        public string ParkName { get; set; }
        public int ParkCount { get; set; }
        public string ParkDescription { get; set; }
    }
}