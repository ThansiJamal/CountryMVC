using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CountryMVC.Models
{
    public class Country
    {
        public int callingCodes { get; set; }
        public string name { get; set; }
        public string capital { get; set; }
        public int population { get; set; }
    }
}