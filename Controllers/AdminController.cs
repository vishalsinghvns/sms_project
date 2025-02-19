using demo.smart_school.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace demo.smart_school.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        Demo_smart_school_DbEntities db = new Demo_smart_school_DbEntities();
        public ActionResult Dashboard()
        {
            return View();
        }

      
    }
}