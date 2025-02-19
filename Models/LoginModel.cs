using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace demo.smart_school.Models
{
    public class LoginModel
    {
        public string Action { get; set; }
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
        public List<LoginModel> lst { get; set; }

        public DataSet GetUserDetails(LoginModel model)
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Id", id)
            };

            DataSet ds = Connection.ExecuteQuery("Sp_SelectLoginData", para);
            return ds;
        }
        public DataSet DeleteUser()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Aid", id),
                                      new SqlParameter("@Action", Action),
            };

            DataSet ds = Connection.ExecuteQuery("DeleteLoginData", para);
            return ds;
        }
    }
}