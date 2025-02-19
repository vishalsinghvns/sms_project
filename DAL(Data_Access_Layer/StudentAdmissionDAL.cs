using demo.smart_school.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace demo.smart_school.DAL
{
    public class StudentAdmissionDAL :Connection
    {

        public void ManageAdmission(string action, StudentAdmission student)
        {
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ManageStudentAdmission", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", action);
                cmd.Parameters.AddWithValue("@id", student.Id);
                cmd.Parameters.AddWithValue("@studentName", student.StudentName);
                cmd.Parameters.AddWithValue("@dob", student.Dob);
                cmd.Parameters.AddWithValue("@gender", student.Gender);
                cmd.Parameters.AddWithValue("@phone", student.Phone);
                cmd.Parameters.AddWithValue("@email", student.Email);
                cmd.Parameters.AddWithValue("@address", student.Address);
                cmd.Parameters.AddWithValue("@admissionDate", student.AdmissionDate);
                cmd.Parameters.AddWithValue("@class", student.Class);
                cmd.Parameters.AddWithValue("@section", student.Section);
                cmd.Parameters.AddWithValue("@parentName", student.ParentName);
                cmd.Parameters.AddWithValue("@parentPhone", student.ParentPhone);
                cmd.Parameters.AddWithValue("@status", student.Status);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

}