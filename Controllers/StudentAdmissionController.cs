using demo.smart_school.DAL;
using demo.smart_school.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demo.smart_school.Controllers
{
    [Authorize]
    public class StudentAdmissionController : Controller
    {
        StudentAdmissionDAL _dal = new StudentAdmissionDAL();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdmissionForm(int? c_id)  // Nullable to handle missing values
        {
            if (c_id == null || c_id == 0) // Check for missing or invalid ID
            {
                return View(new StudentAdmission()); // Return an empty form for new admission
            }

            StudentAdmission student = _dal.GetStudentById(c_id.Value); // Use .Value safely

            if (student == null)
            {
                TempData["Message"] = "Student not found or already deleted!";
                return RedirectToAction("GetStudents");
            }

            return View(student); // Return student details for editing
        }

        [HttpPost]
        public ActionResult AdmissionForm(StudentAdmission student)
        {
            if (!ModelState.IsValid)
            {
                return View(student); // Return form with validation errors
            }

            string operation;
            if (student.Id == 0)
            {
                operation = "INSERT";
                TempData["Message"] = "Student admission added successfully!";
            }
            else
            {
                operation = "UPDATE";
                TempData["Message"] = "Student admission updated successfully!";
            }

            _dal.ManageAdmission(operation, student);
            return RedirectToAction("GetStudents");
        }

        [HttpPost]
        public ActionResult Save(StudentAdmission student)
        {
            _dal.ManageAdmission(student.Id == 0 ? "INSERT" : "UPDATE", student);
            TempData["Message"] = "Student admission saved successfully!";
            return RedirectToAction("GetStudents");
        }

        [HttpGet]
        public ActionResult Edit(int c_id)
        {
            StudentAdmission student = _dal.GetStudentById(c_id);
            if (student == null)
            {
                TempData["Message"] = "Student not found or already deleted!";
                return RedirectToAction("GetStudents");
            }
            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(StudentAdmission student)
        {
            if (ModelState.IsValid)
            {
                string message = _dal.UpdateStudent(student);
                TempData["Message"] = message;
                return RedirectToAction("GetStudents");
            }
            return View(student);
        }


        public ActionResult GetStudents()
        {
            List<StudentAdmission> students = _dal.GetAllStudents();
            return View(students);
        }

        
      
        private readonly StudentDeleteBulkDAL _StudentRepository;
        public StudentAdmissionController()
        {
            _StudentRepository = new StudentDeleteBulkDAL(); // Directly instantiate repository
        }
        public JsonResult SoftDeleteBulkStudents(string userIds)
        {
            bool isDeleted = _StudentRepository.SoftDeleteBulkStudents(userIds);
            TempData["Message"] = "Record deleted successfully!";
            return Json(new { success = isDeleted });
        }

        
        public ActionResult Delete(int id)
        {
            string message = _dal.DeleteStudent(id);
            TempData["Message"] = "Record deleted successfully!";
            return RedirectToAction("GetStudents");
        }



        public ActionResult SearchStudents(string name, string className, string section)
        {
            var students = _dal.SearchStudents(name, className, section);
            TempData["MessageNotFound"] = "Student Not Found!";
            return View("GetStudents", students); // Reuse the GetStudents View
        }
    }

}