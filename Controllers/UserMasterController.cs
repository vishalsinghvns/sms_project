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
   
        // GET: UserMaster
        [Authorize]
        public class UserMasterController : Controller
        {
            Demo_smart_school_DbEntities db = new Demo_smart_school_DbEntities();
        private readonly DeleteBulkDAL _userRepository;

        public UserMasterController()
        {
            _userRepository = new DeleteBulkDAL(); // Directly instantiate repository
        }
        public ActionResult Users()
            {
                LoginModel model = new LoginModel();
                List<LoginModel> lst = new List<LoginModel>();
                DataSet ds = model.GetUserDetails(model);
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        LoginModel obj = new LoginModel();
                        obj.id = Convert.ToInt32(dr["id"].ToString());
                        obj.fullname = dr["fullname"].ToString();
                        obj.email = dr["email"].ToString();
                        obj.mobile = dr["mobile"].ToString();
                        obj.password = dr["password"].ToString();
                        obj.role = dr["role"].ToString();
                        obj.isactive = Convert.ToBoolean(dr["isactive"].ToString());
                        obj.ipname = dr["ipname"].ToString();
                        obj.ipadd = dr["ipadd"].ToString();
                        obj.logindate = dr["logindate"].ToString();
                        lst.Add(obj);
                    }
                    model.lst = lst;
                }
                return View(model);
            }
            [HttpGet]
            public ActionResult AddUser(int c_id = 0)
            {
                ViewBag.shop = new SelectList(db.tblSuperusers.ToList(), "id", "Shop");
                if (c_id != 0)
                {
                    UserModel model = new UserModel();
                    model.Addusers = (from u in db.tblSuperusers.Where(x => x.id == c_id)
                                      select new Users
                                      {
                                          id = u.id,
                                          fullname = u.fullname,
                                          email = u.email,
                                          mobile = u.mobile,
                                          role = u.role,
                                          password = u.password,
                                      }).FirstOrDefault();
                    model.Menuslist1 = (from u in db.tbl_Menus.Where(x => x.mid >= 1 && x.mid <= 7)
                                        select new MenusModel
                                        {
                                            mid = u.mid,
                                            menu_name = u.menu_name
                                        }).ToList();
                    model.Menuslist2 = (from u in db.tbl_Menus.Where(x => x.mid >= 8 && x.mid <= 11)
                                        select new MenusModel
                                        {
                                            mid = u.mid,
                                            menu_name = u.menu_name
                                        }).ToList();
                    model.Menuslist3 = (from u in db.tbl_Menus.Where(x => x.mid >= 12 && x.mid <= 15)
                                        select new MenusModel
                                        {
                                            mid = u.mid,
                                            menu_name = u.menu_name
                                        }).ToList();
                    model.SubmenuList = (from u in db.tbl_SubMenu.Where(x => x.sid >= 1 && x.sid <= 15)
                                         select new SubMenuModel
                                         {
                                             sid = u.sid,
                                             menu_id = u.menu_id,
                                             submenu_name = u.submenu_name,
                                         }).ToList();
                    return View(model);
                }
                else
                {
                    UserModel model2 = new UserModel();
                    model2.Menuslist1 = (from u in db.tbl_Menus.Where(x => x.mid >= 1 && x.mid <= 7)
                                         select new MenusModel
                                         {
                                             mid = u.mid,
                                             menu_name = u.menu_name
                                         }).ToList();
                    model2.Menuslist2 = (from u in db.tbl_Menus.Where(x => x.mid >= 8 && x.mid <= 11)
                                         select new MenusModel
                                         {
                                             mid = u.mid,
                                             menu_name = u.menu_name
                                         }).ToList();
                    model2.Menuslist3 = (from u in db.tbl_Menus.Where(x => x.mid >= 12 && x.mid <= 15)
                                         select new MenusModel
                                         {
                                             mid = u.mid,
                                             menu_name = u.menu_name
                                         }).ToList();
                    return View(model2);
                }

            }
            [HttpGet]
            //Get Assign Permission list
            public JsonResult Getassingrole(int id = 0)
            {
                var list = (from u in db.tbl_UserHasMenus.Where(x => x.userid == id)
                            select new MenusModel
                            {
                                mid = (int)u.m_id,
                            }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

            [HttpPost]
            public ActionResult AddUser(UserModel model)
            {
                int[] arr_menuid = model.Addusers.menuid_arr;
                var GenarateUserVerificationLink = "/Home/Index/";
                var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, GenarateUserVerificationLink);

                var fromMail = new MailAddress("vs7940095@gmail.com", "Send Login Email And Password"); // set your email
                var fromEmailpassword = "ycob cyiy kkrt yaht"; // Set your password
                var toEmail = new MailAddress(model.Addusers.email);
                var smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(fromMail.Address, fromEmailpassword);
                var SendOtp = new MailMessage(fromMail, toEmail);
                SendOtp.Subject = "Your Login Email And Password Password ";
                SendOtp.Body = "<br/> <h1 style='Color:green'>You Are Register Successfully</h1>" + "< br/> <p>please click on the below link for Login</p>" + "<br/><br/><a href=" + link + ">" + link + "</a>" + "<br> Your Login Name : " + model.Addusers.fullname + "<br> Your Login Email : " + model.Addusers.email + "<br> Your Login Password : " + model.Addusers.password;
                SendOtp.IsBodyHtml = true;
                smtp.Send(SendOtp);

                if (model.Addusers.id == 0)
                {
                    //DataSet ds = new DataSet();
                    int maxid = db.tblSuperusers.Max(x => (int?)x.id) ?? 0;
                    int ids = maxid + 1;
                    tblSuperuser ds = new tblSuperuser();
                    ds.id = ids;
                    ds.fullname = model.Addusers.fullname;
                    ds.email = model.Addusers.email;
                    ds.mobile = model.Addusers.mobile;
                    ds.role = model.Addusers.role;
                    ds.password = model.Addusers.password;
                    ds.isactive = false;
                    ds.isDeleted = false;
                    db.tblSuperusers.Add(ds);
                    db.SaveChanges();
                    //tbl.Action = "1";
                    //ds = tbl.InsertAdminLogin();

                    for (int i = 0; i <= arr_menuid.Length - 1; i++)
                    {
                        int maxids = db.tbl_UserHasMenus.Max(x => (int?)x.id) ?? 0;
                        int idss = maxids + 1;
                        tbl_UserHasMenus tbl1 = new tbl_UserHasMenus();
                        tbl1.id = idss;
                        tbl1.userid = ids;
                        tbl1.m_id = arr_menuid[i];
                        db.tbl_UserHasMenus.Add(tbl1);
                        db.SaveChanges();
                    }
                }
                else
                {
                    var updt = db.tblSuperusers.Where(x => x.id == model.Addusers.id).FirstOrDefault();
                    updt.fullname = model.Addusers.fullname;
                    updt.email = model.Addusers.email;
                    updt.mobile = model.Addusers.mobile;
                    updt.role = model.Addusers.role;
                    updt.password = model.Addusers.password;
                    updt.isactive = false;
                    updt.isDeleted = false;
                    db.SaveChanges();

                    var removerole = db.tbl_UserHasMenus.Where(x => x.userid == model.Addusers.id).ToList();
                    if (removerole != null)
                    {
                        foreach (var item in removerole)
                        {
                            db.tbl_UserHasMenus.Remove(item);
                            db.SaveChanges();
                        }
                    }
                    for (int i = 0; i <= arr_menuid.Length - 1; i++)
                    {
                        int maxids = db.tbl_UserHasMenus.Max(x => (int?)x.id) ?? 0;
                        int idss = maxids + 1;
                        tbl_UserHasMenus tbl1 = new tbl_UserHasMenus();
                        tbl1.id = idss;
                        tbl1.userid = model.Addusers.id;
                        tbl1.m_id = arr_menuid[i];
                        db.tbl_UserHasMenus.Add(tbl1);
                        db.SaveChanges();
                    }
                }
                TempData["msg"] = "<script>alert('Insert Successfully')</script>";
                return RedirectToAction("UsersMaster", "Admin");
            }

            public ActionResult DeleteUser(string Id)
            {
                string FormName = "Users";
                string Controller = "UserMaster";
                try
                {
                    LoginModel obj = new LoginModel();
                    obj.id = Convert.ToInt32(Id);
                    obj.Action = "1";
                    DataSet ds = obj.DeleteUser();

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["msg"] = "Reg deleted successfully";
                            FormName = "UsersMaster";
                            Controller = "Admin";
                        }
                        else
                        {
                            TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            FormName = "UsersMaster";
                            Controller = "Admin";
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["msg"] = ex.Message;
                    FormName = "UsersMaster";
                    Controller = "Admin";
                }
                return RedirectToAction(FormName, Controller);
            }

        [HttpPost]
        public JsonResult SoftDeleteBulkUsers(string userIds)
        {
            bool isDeleted = _userRepository.SoftDeleteBulkUsers(userIds);
            return Json(new { success = isDeleted });
        }

        }
    }
