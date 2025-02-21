using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

    public class StudentDeleteBulkDAL : Connection
    {
    public bool SoftDeleteBulkStudents(string userIds)
    {
        using (SqlConnection conn = GetConnection())
        {
            using (SqlCommand cmd = new SqlCommand("SoftDeleteBulkStudent", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentAdmissionIDs", userIds);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();

                return rowsAffected > 0;
            }
        }
    }

    internal bool SoftDeleteBulkUsers(List<int> idList)
    {
        throw new NotImplementedException();
    }
}
