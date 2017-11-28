using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;

namespace BuisnessLayer
{
    public class StudentBusinessLayer
    {//dbcontext
        public IEnumerable<Student> Students
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["CON"].ConnectionString;
                List<Student> students = new List<Student>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    SqlCommand cmd = new SqlCommand("usp_GetStudents", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Student student = new Student();
                        student.StudentId = Convert.ToInt32(rdr["StudentId"]);
                        student.StudentName = rdr["StudentName"].ToString();
                        students.Add(student);
                    }

                    return students;
                }
            }
        }

        public string UpdateAttendance(List<Student> students, DateTime selDate)
        {
            string message = "Attendance has been captured for the date " + selDate.ToShortDateString().ToString();
                string connectionString = ConfigurationManager.ConnectionStrings["CON"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                   
                    SqlCommand cmd = new SqlCommand("usp_UpdateAttendance", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (Student student in students)
                    {
                        SqlParameter paramId = new SqlParameter();
                        paramId.ParameterName = "@studentId";
                        paramId.Value = student.StudentId;
                        cmd.Parameters.Add(paramId);

                        SqlParameter paramName = new SqlParameter();
                        paramName.ParameterName = "@studentName";
                        paramName.Value = student.StudentName;
                        cmd.Parameters.Add(paramName);

                        SqlParameter paramAttendanceDate = new SqlParameter();
                        paramAttendanceDate.ParameterName = "@attendanceDate";
                        paramAttendanceDate.Value = selDate;
                        cmd.Parameters.Add(paramAttendanceDate);

                        SqlParameter paramAttendanceStatus = new SqlParameter();
                        paramAttendanceStatus.ParameterName = "@attendanceStatus";
                        paramAttendanceStatus.Value = student.AttendanceStatus;
                        cmd.Parameters.Add(paramAttendanceStatus);

                        con.Open();
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch
                        {
                            message = "Attendance was already recorded for the date " + selDate.ToShortDateString().ToString();
                            break;
                        }
                        con.Close();
                        cmd.Parameters.Clear();
                    }
                    return message;
                }
   
       
        }


        public IEnumerable<Student> StudentsAttendance(DateTime selDate)
        {
            {
                string connectionString = ConfigurationManager.ConnectionStrings["CON"].ConnectionString;
                List<Student> students = new List<Student>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    SqlCommand cmd = new SqlCommand("usp_GetStudentsAttendance", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@attendanceDate",selDate));
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Student student = new Student();
                        student.StudentId = Convert.ToInt32(rdr["StudentId"]);
                        student.StudentName = rdr["StudentName"].ToString();
                        string isPresent = rdr["AttendanceStatus"].ToString();
                        if (isPresent=="1") 
                        {
                            student.AttendanceStatus=true;
                        }
                        else 
                        {
                            student.AttendanceStatus=false;
                        }
                        students.Add(student);
                    }

                    return students;
                }
            }
        }

        public IEnumerable<Attendnace> GetAttendanceByStudent(int SelID)
        {
            {
                string connectionString = ConfigurationManager.ConnectionStrings["CON"].ConnectionString;
                List<Attendnace> attendnaceList = new List<Attendnace>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    SqlCommand cmd = new SqlCommand("usp_GetAttendanceByStudent", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@studentId", SelID));
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Attendnace attendance = new Attendnace();
                        attendance.AttendanceDate = Convert.ToDateTime(rdr["AttendanceDate"]).ToShortDateString();
                        string isPresent = rdr["AttendanceStatus"].ToString();
                        if (isPresent == "1")
                        {
                            attendance.AttendanceStatus = true;
                        }
                        else
                        {
                            attendance.AttendanceStatus = false;
                        }
                        attendnaceList.Add(attendance);
                    }

                    return attendnaceList;
                }
            }
        }
    }
}
