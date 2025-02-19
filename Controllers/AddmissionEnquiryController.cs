using demo.smart_school.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demo.smart_school.Controllers
{
    public class AdmissionEnquiryController : Controller
    {
        AdmissionEnquiryDAL _dal = new AdmissionEnquiryDAL();

        public ActionResult Index()
        {
            List<AdmissionEnquiry> enquiries = _dal.GetAdmissionEnquiries();
            return View(enquiries);
        }

        [HttpPost]
        public ActionResult Save(AdmissionEnquiry enquiry)
        {
            string message = _dal.ManageAdmissionEnquiry(enquiry.id == 0 ? "INSERT" : "UPDATE", enquiry);
            TempData["Message"] = message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            string message = _dal.ManageAdmissionEnquiry("DELETE", new AdmissionEnquiry { id = id });
            TempData["Message"] = message;
            return RedirectToAction("Index");
        }
    }

}