using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


    public class DeleteBulkDAL : Connection
    {
    public bool SoftDeleteBulkUsers(string userIds)
    {
        using (SqlConnection conn = GetConnection())
        {
            using (SqlCommand cmd = new SqlCommand("SoftDeleteBulkUsers", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserIDs", userIds);

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
