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
                cmd.Parameters.AddWithValue("@status", "Active");

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public StudentAdmission GetStudentById(int id)
        {
            StudentAdmission student = null;

            using (SqlConnection con = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("sp_ManageStudentAdmission", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "GET_BY_ID");
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            student = new StudentAdmission
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                StudentID = dr["StudentID"].ToString(),
                                RollNo = Convert.ToInt32(dr["RollNo"]),
                                StudentName = dr["StudentName"].ToString(),
                                Dob = Convert.ToDateTime(dr["Dob"]),
                                Gender = dr["Gender"].ToString(),
                                Phone = dr["Phone"].ToString(),
                                Email = dr["Email"].ToString(),
                                Address = dr["Address"].ToString(),
                                AdmissionDate = Convert.ToDateTime(dr["AdmissionDate"]),
                                Class = dr["Class"].ToString(),
                                Section = dr["Section"].ToString(),
                                ParentName = dr["ParentName"].ToString(),
                                ParentPhone = dr["ParentPhone"].ToString(),
                                Status = dr["Status"].ToString()
                            };
                        }
                    }
                }
            }
            return student;
        }
        public string UpdateStudent(StudentAdmission student)
        {
            string message = "";

            using (SqlConnection con = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("sp_ManageStudentAdmission", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "UPDATE");
                    cmd.Parameters.AddWithValue("@Id", student.Id);
                    cmd.Parameters.AddWithValue("@StudentName", student.StudentName);
                    cmd.Parameters.AddWithValue("@Dob", student.Dob);
                    cmd.Parameters.AddWithValue("@Gender", student.Gender);
                    cmd.Parameters.AddWithValue("@Phone", student.Phone);
                    cmd.Parameters.AddWithValue("@Email", student.Email);
                    cmd.Parameters.AddWithValue("@Address", student.Address);
                    cmd.Parameters.AddWithValue("@AdmissionDate", student.AdmissionDate);
                    cmd.Parameters.AddWithValue("@Class", student.Class);
                    cmd.Parameters.AddWithValue("@Section", student.Section);
                    cmd.Parameters.AddWithValue("@ParentName", student.ParentName);
                    cmd.Parameters.AddWithValue("@ParentPhone", student.ParentPhone);
                    cmd.Parameters.AddWithValue("@Status", "Active");

                    con.Open();
                    message = cmd.ExecuteScalar()?.ToString();
                }
            }
            return message;
        }

        public List<StudentAdmission> GetAllStudents()
        {
            List<StudentAdmission> students = new List<StudentAdmission>();

            using (SqlConnection con = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ManageStudentAdmission", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "READ");

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    students.Add(new StudentAdmission
                    {
                        Id = Convert.ToInt32(rdr["id"]),
                        StudentID = rdr["studentID"].ToString(),
                        RollNo = Convert.ToInt32(rdr["rollNo"]),
                        StudentName = rdr["studentName"].ToString(),
                        Dob = Convert.ToDateTime(rdr["dob"]),
                        Gender = rdr["gender"].ToString(),
                        Phone = rdr["phone"].ToString(),
                        Email = rdr["email"].ToString(),
                        Address = rdr["address"].ToString(),
                        AdmissionDate = Convert.ToDateTime(rdr["admissionDate"]),
                        Class = rdr["class"].ToString(),
                        Section = rdr["section"].ToString(),
                        ParentName = rdr["parentName"].ToString(),
                        ParentPhone = rdr["parentPhone"].ToString(),
                        Status = rdr["status"].ToString(),
                    });
                }
            }
            return students;
        }

        public string DeleteStudent(int id)
        {
            string message = "";

            using (SqlConnection con = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("sp_ManageStudentAdmission", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "DELETE");
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    message = cmd.ExecuteScalar()?.ToString();
                }
            }
            return message;
        }


        public List<StudentAdmission> SearchStudents(string name, string className, string section)
        {
            List<StudentAdmission> students = new List<StudentAdmission>();

            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("sp_SearchStudents", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", string.IsNullOrEmpty(name) ? (object)DBNull.Value : name);
                    cmd.Parameters.AddWithValue("@Class", string.IsNullOrEmpty(className) ? (object)DBNull.Value : className);
                    cmd.Parameters.AddWithValue("@Section", string.IsNullOrEmpty(section) ? (object)DBNull.Value : section);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        students.Add(new StudentAdmission
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            StudentID = reader["studentID"].ToString(),
                            RollNo = Convert.ToInt32(reader["rollNo"]),
                            StudentName = reader["studentName"].ToString(),
                            Class = reader["class"].ToString(),
                            Section = reader["section"].ToString(),
                            Status = reader["status"].ToString(),
                            // Add other necessary fields
                        });
                    }
                }
            }

            return students;
        }


    }

}