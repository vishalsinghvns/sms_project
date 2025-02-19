using demo.smart_school.DAL;
using demo.smart_school.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demo.smart_school.Controllers
{
    public class StudentAdmissionController : Controller
    {
        StudentAdmissionDAL _dal = new StudentAdmissionDAL();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(StudentAdmission student)
        {
            _dal.ManageAdmission(student.Id == 0 ? "INSERT" : "UPDATE", student);
            TempData["Message"] = "Student admission saved successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _dal.ManageAdmission("DELETE", new StudentAdmission { Id = id });
            TempData["Message"] = "Student admission deleted successfully!";
            return RedirectToAction("Index");
        }
    }

}