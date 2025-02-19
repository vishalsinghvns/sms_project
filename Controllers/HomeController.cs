using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using demo.smart_school.Models;
using SRS_Task.Filter;

namespace demo.smart_school.Controllers
{
    public class HomeController : Controller
    {
        Demo_smart_school_DbEntities db = new Demo_smart_school_DbEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginModel obj, string ReturnUrl)
        {
            //var paas = Crypto.Hash(obj.Password);
            var IsValid = db.tblSuperusers.Where(x => x.email == obj.email && x.password == obj.password).FirstOrDefault();
            if (IsValid != null && IsValid.isDeleted == false)
            {
                Session["email"] = IsValid.email.ToString();
                var email = Session["email"];
                var ds = db.tblSuperusers.Where(x => x.email == IsValid.email).FirstOrDefault();
                ds.isactive = true;
                ds.ipname = GetIPName.GetHostName();
                ds.ipadd = GetIPName.getIp();
                ds.logindate = DateTime.Now.ToString();
                db.SaveChanges();
                FormsAuthentication.SetAuthCookie(IsValid.fullname, false);
                Session["fullname"] = IsValid.fullname.ToString();
                Session["Role"] = IsValid.role.ToString();
                Session["Online"] = IsValid.isactive.ToString();
                Session["uid"] = ds.id;
                List<int?> list = db.tbl_UserHasMenus.Where(x => x.userid == IsValid.id).Select(x => x.m_id).ToList();
                int?[] arr = list.ToArray();
                Session["menu"] = arr;
                List<int?> list1 = db.tbl_UserHasMenus.Where(x => x.userid == IsValid.id).Select(x => x.m_id).ToList();
                int?[] arr1 = list1.ToArray();
                Session["submenu"] = arr1;
                if (Convert.ToBoolean(Session["Online"]) == true && Session["uid"] != null)
                {
                    Session["OnlineUser"] = IsValid.isactive.ToString();
                }
                else
                {
                    Session["OfflineUser"] = IsValid.isactive.ToString();
                }
                if (ReturnUrl != null)
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
            }
            else
            {
                ModelState.AddModelError("Error", "The username or password provided is incorrect.");
                return View();
            }
        }

        public ActionResult Logout()
        {
            var email = Session["email"];
            var del = db.tblSuperusers.Where(x => x.email == email).FirstOrDefault();
            del.isactive = false;
            db.SaveChanges();
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}