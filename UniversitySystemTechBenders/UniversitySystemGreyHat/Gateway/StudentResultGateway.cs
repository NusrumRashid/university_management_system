using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementWebApp.Models.ViewModel;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Gateway
{
    public class StudentResultGateway:BaseGateway
    {
        public int Save(StudentResult result)
        {
            string query = "INSERT INTO StudentResult VALUES(@studentId,@courseId,@gradeID)";
            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@studentId", result.StudentId);
            Command.Parameters.AddWithValue("@courseId", result.CourseId);
            Command.Parameters.AddWithValue("@gradeID", result.GradeLetterId);
            

            Connection.Open();
            int rowAffect = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffect;

        }

        public bool IsCourseResultSaved(StudentResult result)
        {

            string query = "SELECT * FROM StudentResult WHERE  StudentId=@studentId AND CourseId=@courseId";

            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@studentId", result.StudentId);
            Command.Parameters.AddWithValue("@courseId", result.CourseId);



            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExists = Reader.HasRows;
            Connection.Close();
            return isExists;

        }
        public int UpdateGrade(StudentResult result)
        {


            string credit = "UPDATE StudentResult SET GradeId=@gradeid WHERE StudentId=@studentId AND CourseId=@courseId ";
            Command = new SqlCommand(credit, Connection);
            Command.Parameters.AddWithValue("@studentId", result.StudentId);
            Command.Parameters.AddWithValue("@courseId", result.CourseId);

            Command.Parameters.AddWithValue("@gradeid", result.GradeLetterId);
            Connection.Open();

            int rowAffected = Command.ExecuteNonQuery();



            Connection.Close();
            return rowAffected;
        }
        public List<StudentViewModel> GetCourseById(int studentId)
        {

            string query = @"SELECT        EnrollCourse.CourseId AS CourseId, Course.Code AS CourseCode, Course.Name AS CourseName
FROM            EnrollCourse INNER JOIN
                         Course ON EnrollCourse.CourseId = Course.CourseId INNER JOIN
                         Student ON EnrollCourse.StudentId = Student.StudentId WHERE Student.StudentId=" + studentId;

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
        public List<StudentViewModel> GetResultList(int studentId)
        {

            string query = @"SELECT        A.CourseName, A.CourseCode, A.CourseId, A.StudentId, StudentResult.GradeId, GradeLetter.GradeLetterName AS Grade
FROM            GradeLetter INNER JOIN
                         StudentResult ON GradeLetter.GradeLetterId = StudentResult.GradeId RIGHT OUTER JOIN
                             (SELECT DISTINCT Course.Name AS CourseName, Course.Code AS CourseCode, EnrollCourse.CourseId, EnrollCourse.StudentId
                               FROM            EnrollCourse LEFT OUTER JOIN
                                                         Course ON EnrollCourse.CourseId = Course.CourseId
                               WHERE        (EnrollCourse.StudentId = '" +studentId+"')) AS A ON StudentResult.StudentId = A.StudentId AND StudentResult.CourseId = A.CourseId";

            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();


            List<StudentViewModel> studentList = new List<StudentViewModel>();
            while (Reader.Read())
            {
                StudentViewModel student = new StudentViewModel();

                student.CourseCode = Reader["CourseCode"].ToString();
                student.CourseName = Reader["CourseName"].ToString();
                if (Reader["Grade"].ToString()!="")
                {
                    student.Grade = Reader["Grade"].ToString();
                    
                }
                else
                {
                    student.Grade = "Not Graded Yet";
                    
                }



                studentList.Add(student);
            }
            Connection.Close();
            return studentList;

        }
    }
}