using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo.smart_school.Models
{
    public class AdmissionEnquiryModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string descri { get; set; }
        public string note { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<System.DateTime> nextfdate { get; set; }
        public string assigned { get; set; }
        public string reference { get; set; }
        public string sourc { get; set; }
        public string @class { get; set; }
        public string noofchild { get; set; }
        public string status { get; set; }
        public Nullable<bool> isDeleted { get; set; }
    }
}