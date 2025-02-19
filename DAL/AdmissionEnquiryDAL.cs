using demo.smart_school.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class AdmissionEnquiryDAL :Connection
{

    public string ManageAdmissionEnquiry(string action, AdmissionEnquiry enquiry)
    {
        string message = "";
        using (SqlConnection conn = GetConnection())
        {
            SqlCommand cmd = new SqlCommand("sp_ManageAdmissionEnquiry", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Action", action);
            cmd.Parameters.AddWithValue("@id", enquiry.id);
            cmd.Parameters.AddWithValue("@name", enquiry.name);
            cmd.Parameters.AddWithValue("@phone", enquiry.phone);
            cmd.Parameters.AddWithValue("@email", enquiry.email);
            cmd.Parameters.AddWithValue("@address", enquiry.address);
            cmd.Parameters.AddWithValue("@descri", enquiry.description);
            cmd.Parameters.AddWithValue("@note", enquiry.note);
            cmd.Parameters.AddWithValue("@date", enquiry.date);
            cmd.Parameters.AddWithValue("@nextfdate", enquiry.nextfdate);
            cmd.Parameters.AddWithValue("@assigned", enquiry.assigned);
            cmd.Parameters.AddWithValue("@reference", enquiry.reference);
            cmd.Parameters.AddWithValue("@sourc", enquiry.sourc);
            cmd.Parameters.AddWithValue("@classs", enquiry.classs);
            cmd.Parameters.AddWithValue("@noofchild", enquiry.noofchild);
            cmd.Parameters.AddWithValue("@status", enquiry.status);

            conn.Open();
            message = cmd.ExecuteScalar()?.ToString();
        }
        return message;
    }

    public List<AdmissionEnquiry> GetAdmissionEnquiries()
    {
        List<AdmissionEnquiry> enquiries = new List<AdmissionEnquiry>();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_ManageAdmissionEnquiry", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "READ");

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                enquiries.Add(new AdmissionEnquiry
                {
                    id = Convert.ToInt32(reader["id"]),
                    name = reader["name"].ToString(),
                    phone = reader["phone"].ToString(),
                    email = reader["email"].ToString(),
                    address = reader["address"].ToString(),
                    description = reader["descri"].ToString(),
                    note = reader["note"].ToString(),
                    date = Convert.ToDateTime(reader["date"]),
                    nextfdate = Convert.ToDateTime(reader["nextfdate"]),
                    assigned = reader["assigned"].ToString(),
                    reference = reader["reference"].ToString(),
                    sourc = reader["sourc"].ToString(),
                    classs = reader["classs"].ToString(),
                    noofchild = Convert.ToInt32(reader["noofchild"]),
                    status = reader["status"].ToString()
                });
            }
        }
        return enquiries;
    }
}

