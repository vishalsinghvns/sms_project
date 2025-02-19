using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo.smart_school.Models
{
    public class AdmissionEnquiry
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string description { get; set; }
        public string note { get; set; }
        public DateTime date { get; set; }
        public DateTime nextfdate { get; set; }
        public string assigned { get; set; }
        public string reference { get; set; }
        public string sourc { get; set; }
        public string classs { get; set; }
        public int noofchild { get; set; }
        public string status { get; set; }
        public string message { get; set; } // For success message
    }
}