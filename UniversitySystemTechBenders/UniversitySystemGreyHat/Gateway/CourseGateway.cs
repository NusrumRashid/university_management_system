using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Gateway
{
    public class CourseGateway:BaseGateway
    {
        public int Save(Course course)
        {
            string query = "INSERT INTO Course VALUES(@code,@name,@credit,@description,@departmentId,@semesterId,@status)";
            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@code", course.Code);
            Command.Parameters.AddWithValue("@name", course.Name);
            Command.Parameters.AddWithValue("@credit", course.Credit);
            Command.Parameters.AddWithValue("@description", course.Description);
            Command.Parameters.AddWithValue("@departmentId", course.DepartmentId);
            Command.Parameters.AddWithValue("@semesterId", course.SemesterId);
            Command.Parameters.AddWithValue("@status", course.Status);

            Connection.Open();
            int rowAffect = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffect;

        }

        public bool IsCourseExists(Course course)
        {
            string status = "Assigned";
            string query = "SELECT * FROM Course WHERE Code=@code   AND DepartmentId=@departmentId AND Status=@status";

            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@code", course.Code);
            Command.Parameters.AddWithValue("@name", course.Name);
            Command.Parameters.AddWithValue("@status", status);
            Command.Parameters.AddWithValue("@departmentId", course.DepartmentId);

            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExists = Reader.HasRows;
            Connection.Close();
            return isExists;

        } 
        //public bool IsCourseNameExists(Course course)
        //{
        //    string status = "Assigned";
        //    string query = "SELECT * FROM Course WHERE Name=@name AND Status=@status";

        //    Command = new SqlCommand(query, Connection);
        //    Command.Parameters.AddWithValue("@code", course.Code);
        //    Command.Parameters.AddWithValue("@name", course.Name);
        //    Command.Parameters.AddWithValue("@status", status);

        //    Connection.Open();
        //    Reader = Command.ExecuteReader();
        //    bool isExists = Reader.HasRows;
        //    Connection.Close();
        //    return isExists;

        //}
        public bool IsCourseCodeExists(Course course)
        {
            string status = "Assigned";
            string query = "SELECT * FROM Course WHERE Code=@code AND Status=@status";

            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@code", course.Code);
            Command.Parameters.AddWithValue("@name", course.Name);
            Command.Parameters.AddWithValue("@status", status);

            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExists = Reader.HasRows;
            Connection.Close();
            return isExists;

        }

        public List<Department> GetAllDepartments()
        {
            string query = "SELECT * FROM Department ";

            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();


            List<Department> departmentList = new List<Department>();
            while (Reader.Read())
            {
                Department department = new Department();

                department.DepartmentId = Convert.ToInt32(Reader["DepartmentId"]);
                department.DepartmentCode = Reader["DepartmentCode"].ToString();
                department.DepartmentName = Reader["DepartmentName"].ToString();


                departmentList.Add(department);
            }
            Connection.Close();
            return departmentList;

        }

        public List<Semester> GetAllSemesters()
        {
            string query = "SELECT * FROM Semester ";

            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();


            List<Semester> semesterList = new List<Semester>();
            while (Reader.Read())
            {
               Semester semester=new Semester();

                semester.SemesterId= Convert.ToInt32(Reader["SemesterId"]);
                semester.Name = Reader["Name"].ToString();
             

                semesterList.Add(semester);
            }
            Connection.Close();
            return semesterList;

        }
        public int UnAssign()
        {
            string query1 = "Update AssignCourseToTeacher SET Status= 'Not Assigned'";
            Command1 = new SqlCommand(query1, Connection);
            string query2 = "Update Course SET Status= 'Not Assigned'";
            Command2 = new SqlCommand(query2, Connection);
            string query3 = "Update EnrollCourse SET Status= 'Not Assigned'";
            Command3 = new SqlCommand(query3, Connection);
            Connection.Open();
            int rowAffect1 = Command1.ExecuteNonQuery();
            int rowAffect2 = Command2.ExecuteNonQuery();
            int rowAffect3 = Command3.ExecuteNonQuery();
            int rowAffect = rowAffect1 + rowAffect2 + rowAffect3;
            Connection.Close();
            return rowAffect;
        }

        public bool IsCourseUnAssigned1()
        {
            string query = "SELECT * FROM AssignCourseToTeacher WHERE  Status='Not Assigned'";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExists = Reader.HasRows;
            Connection.Close();
            return isExists;
        }

        public bool IsCourseUnAssigned2()
        {
            string query = "SELECT * FROM Course WHERE  Status='Not Assigned'";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExists = Reader.HasRows;
            Connection.Close();
            return isExists;
        }

        public bool IsCourseUnAssigned3()
        {
            string query = "SELECT * FROM EnrollCourse WHERE  Status='Not Assigned'";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExists = Reader.HasRows;
            Connection.Close();
            return isExists;
        }
    }
}