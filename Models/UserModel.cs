using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo.smart_school.Models
{
    public class UserModel
    {
        public virtual List<Users> Userlist { get; set; }
        public virtual List<MenusModel> Menuslist1 { get; set; }
        public virtual List<MenusModel> Menuslist2 { get; set; }
        public virtual List<MenusModel> Menuslist3 { get; set; }
        public virtual List<SubMenuModel> SubmenuList { get; set; }
        public virtual List<UserHasMenus> UserMenulist { get; set; }
        public virtual Users Addusers { get; set; }
    }
    public class Users
    {
        public int id { get; set; }
        public string fullname { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Nullable<bool> isDeleted { get; set; }
        public string role { get; set; }
        public string ipname { get; set; }
        public string ipadd { get; set; }
        public string logindate { get; set; }
        public Nullable<bool> isactive { get; set; }
        public int[] menuid_arr { get; set; }
        public int[] submenuid_arr { get; set; }
    }
    public class MenusModel
    {
        public int mid { get; set; }
        public string menu_name { get; set; }
    }
    public class SubMenuModel
    {
        public int sid { get; set; }
        public Nullable<int> menu_id { get; set; }
        public string submenu_name { get; set; }
    }
    public class UserHasMenus
    {
        public int id { get; set; }
        public Nullable<int> userid { get; set; }
        public Nullable<int> m_id { get; set; }
        public Nullable<int> s_id { get; set; }
    }
}