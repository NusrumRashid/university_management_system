using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementWebApp.Models;
using UniversityManagementWebApp.Models.ViewModel;
using UniversitySystemTechBenders.Models.ViewModel;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Gateway
{
    public class EnrollCourseGateway:BaseGateway
    {
        public List<StudentModel> GetAllRegistrationNo()
        {
            string query = "SELECT StudentId,RegistrationNo FROM Student ";

            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();


            List<StudentModel> studentList = new List<StudentModel>();
            while (Reader.Read())
            {
                StudentModel student = new StudentModel();

                student.StudentId = Convert.ToInt32(Reader["StudentId"]);
                student.RegistrationNo = Reader["RegistrationNo"].ToString();
                


                studentList.Add(student);
            }
            Connection.Close();
            return studentList;

        }

        public StudentViewModel GetStudentById(int studentId)
        {
            string query ="SELECT Student.StudentId, Student.RegistrationNo, Student.Name, Student.Email, Student.DepartmentId, Department.DepartmentName, Department.DepartmentCode FROM Student INNER JOIN Department ON Student.DepartmentId = Department.DepartmentId WHERE Student.StudentId=" +studentId;
            Command = new SqlCommand(query, Connection);

            Connection.Open();
            Reader = Command.ExecuteReader();
            Reader.Read();
            StudentViewModel student = null;

            if (Reader.HasRows)
            {
                student = new StudentViewModel();
                student.StudentId = Convert.ToInt32(Reader["StudentId"]);
                student.Name = (Reader["Name"]).ToString();
                student.Email = (Reader["Email"]).ToString();
                student.DepartmentId = Convert.ToInt32(Reader["DepartmentId"]);
                student.DepartmentName = (Reader["DepartmentName"]).ToString();
                student.DepartmentCode = (Reader["DepartmentCode"]).ToString();
            }

            Reader.Close();
            Connection.Close();
            return student;
        }

        public List<StudentViewModel> GetCourseByDepartmentCode(string registrationNo)
        {

            string query = @"SELECT  DISTINCT      Course.CourseId AS CourseId, Course.Code AS CourseCode, Course.Name AS CourseName
FROM            Course INNER JOIN
                         Department ON Course.DepartmentId = Department.DepartmentId
						 LEFT JOIN  Student ON Department.DepartmentId=Student.DepartmentId  WHERE Department.DepartmentCode=(SUBSTRING('" + registrationNo + "',1,3))";

            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();


            List<StudentViewModel> studentList = new List<StudentViewModel>();
            while (Reader.Read())
            {
                StudentViewModel student = new StudentViewModel();


                student.CourseId = Convert.ToInt32(Reader["CourseId"]);
          
                student.CourseCode = Reader["CourseCode"].ToString();
                student.CourseName = Reader["CourseName"].ToString();



                studentList.Add(student);
            }
            Connection.Close();
            return studentList;

        }
        public static DateTime TimeZone(DateTime datetime)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(datetime, timeZoneInfo);
            return PrintDate;
        }

        public int Save(EnrollCourse enroll)
        {
            string query = "INSERT INTO EnrollCourse VALUES(@studentId,@courseId,@date,@status)";
            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@studentId", enroll.StudentId);
            Command.Parameters.AddWithValue("@courseId", enroll.CourseId);
            Command.Parameters.AddWithValue("@date", enroll.Date);
            Command.Parameters.AddWithValue("@status", enroll.Status);
           
            Connection.Open();
            int rowAffect = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffect;

        }

        public bool IsCourseEnrolled(EnrollCourse enroll)
        {

            string query = "SELECT * FROM EnrollCourse WHERE  StudentId=@studentId AND CourseId=@courseId";

            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@studentId", enroll.StudentId);
            Command.Parameters.AddWithValue("@courseId", enroll.CourseId);
            


            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExists = Reader.HasRows;
            Connection.Close();
            return isExists;

        }
    }
}