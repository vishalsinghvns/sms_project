using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo.smart_school.Models
{
    public class StudentAdmission
    {
        public int Id { get; set; }
        public string StudentID { get; set; }
        public int RollNo { get; set; }
        public string StudentName { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
        public string ParentName { get; set; }
        public string ParentPhone { get; set; }
        public string Status { get; set; }
    }

}