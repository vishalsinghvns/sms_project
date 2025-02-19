using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace demo.smart_school.Models
{
    public class Connection
    {
        public static string connectionString = string.Empty;

        static Connection()
        {
            try
            {
                connectionString = "Data Source=Vishal\\SQLEXPRESS;Initial Catalog=Demo_smart_school_Db; Integrated Security=True;";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int ExecuteNonQuery(string commandText, params SqlParameter[] commandParameters)
        {
            int k = 0;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(commandParameters);
                    connection.Open();
                    k = command.ExecuteNonQuery();
                }
                return k;
            }
            catch
            {
                return k;
            }
        }

        public static DataSet ExecuteQuery(string commandText, params SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(ds);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Msg");
                dt.Columns.Add("ErrorMessage");

                DataRow dr = dt.NewRow();
                dr["Msg"] = "0";
                dr["ErrorMessage"] = ex.Message;
                dt.Rows.Add(dr);
                ds.Tables.Add(dt);

            }
            return ds;
        }
    }
}