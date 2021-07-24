using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversitySystemTechBenders.Models;
using UniversitySystemTechBenders.Models.ViewModel;

namespace UniversitySystemTechBenders.Gateway
{
    public class AssignCourseToTeacherGateway : BaseGateway
    {
        public List<TeacherViewModel> GetTacherByDepartmentId(int departmentId)
        {
            string query = @"SELECT    Teacher.TeacherId AS TeacherId, Teacher.Name AS TeacherName, Teacher.Address AS Address, Teacher.Email AS Email, Teacher.ContactNumber AS ContactNumber,
Teacher.DesignationId AS DesignationId, Teacher.DepartmentId AS DepartmentId, Teacher.TakenCredit AS TakenCredit,
Department.DepartmentName AS DepartmentName,Department.DepartmentCode AS DepartmentCode FROM Teacher INNER JOIN
                         Department ON Teacher.DepartmentId = Department.DepartmentId WHERE Department.DepartmentId=" +
                           departmentId;

            Command = new SqlCommand(query, Connection);
            
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<TeacherViewModel> teacherViewModels = new List<TeacherViewModel>();

            while (Reader.Read())
            {
                TeacherViewModel teacher = new TeacherViewModel();
                teacher.DepartmentId = Convert.ToInt32(Reader["DepartmentId"]);
                teacher.DepartmentCode = Reader["DepartmentCode"].ToString();
                teacher.DepartmentName = Reader["DepartmentName"].ToString();
                teacher.TeacherId = Convert.ToInt32(Reader["TeacherId"]);
                teacher.TeacherName = Reader["TeacherName"].ToString();
                teacher.Address = Reader["Address"].ToString();
                teacher.Email = Reader["Email"].ToString();

                teacher.ContactNumber = Reader["ContactNumber"].ToString();
                teacher.DesignationId = Convert.ToInt32(Reader["DesignationId"]);
                teacher.TakenCredit = Convert.ToDouble(Reader["TakenCredit"]);



                teacherViewModels.Add(teacher);
            }

            Reader.Close();
            Connection.Close();
            return teacherViewModels;
        }
        public TeacherViewModel GetTakenCredit(int teacherId)
        {
            string query = @"SELECT        TakenCredit, TeacherId, Name
FROM            Teacher WHERE Teacher.TeacherId=" + teacherId;

            Command = new SqlCommand(query, Connection);
            
            Connection.Open();
            Reader = Command.ExecuteReader();
            Reader.Read();
            TeacherViewModel teacher = null;

            if (Reader.HasRows)
            {
                teacher = new TeacherViewModel();
                teacher.TeacherId = Convert.ToInt32(Reader["TeacherId"]);
               
                teacher.TakenCredit = Convert.ToDouble(Reader["TakenCredit"]);
       
            }

            Reader.Close();
            Connection.Close();
            return teacher;
        }

        public TeacherViewModel GetCredit(int teacherId)
        {
            string query = @"SELECT  * ,(SELECT SUM(TCR)-(SUM(RCR)) RemainingCredit FROM (
SELECT SUM(TakenCredit) TCR,0 RCR  FROM Teacher WHERE Teacher.TeacherId=X.TeacherId


UNION
SELECT     0 TCR, SUM(Course.Credit)RCR   FROM         AssignCourseToTeacher    INNER JOIN
                         Course ON AssignCourseToTeacher.CourseId = Course.CourseId WHERE  AssignCourseToTeacher.TeacherId=X.TeacherId

)A) AS RemainingCredit FROM (


SELECT   DISTINCT     Teacher.TeacherId AS TeacherId,Teacher.Name AS Name,Teacher.Address AS Address, Teacher.Email AS Email,Teacher.ContactNumber,Teacher.DesignationId AS DesignationId, Course.CourseId AS CourseId, Teacher.DepartmentId AS DepartmentID, Course.Credit, Teacher.TakenCredit
FROM            Teacher left JOIN
                         AssignCourseToTeacher ON Teacher.TeacherId = AssignCourseToTeacher.TeacherId left JOIN
                         Course ON AssignCourseToTeacher.CourseId = Course.CourseId WHERE  Teacher.TeacherId='"+teacherId+"')X ";
            
            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@teacherId", teacherId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            Reader.Read();
            TeacherViewModel teacher = null;

            if (Reader.HasRows)
            {
                teacher = new TeacherViewModel();
                teacher.TeacherId = Convert.ToInt32(Reader["TeacherId"]);
                teacher.TeacherName = (Reader["Name"]).ToString();
                teacher.Address = (Reader["Address"]).ToString();
                teacher.Email = (Reader["Email"]).ToString();
                teacher.ContactNumber = (Reader["ContactNumber"]).ToString();
                teacher.DesignationId = Convert.ToInt32(Reader["DesignationId"]);
                teacher.DepartmentId = Convert.ToInt32(Reader["DepartmentId"]);
                teacher.TakenCredit = Convert.ToDouble(Reader["TakenCredit"]);
                if (Reader["RemainingCredit"]==null)
                {
                    teacher.RemainingCredit = Convert.ToDouble(Reader["TakenCredit"]); ;
                }
                else if (Reader["RemainingCredit"] != null)
                {
                    teacher.RemainingCredit = Convert.ToDouble(Reader["RemainingCredit"]);
                    
                }
                
            }
           

            Reader.Close();
            Connection.Close();
            return teacher;
        }

        public List<CourseStatisticsViewModel> ViewCourseStatistics(int departmentId)
        {
            string query = @"SELECT        c.Code AS CourseCode, c.Name AS CourseName,  c.DepartmentId DepartmentId,  Semester.Name AS SemesterName,Teacher.Name AS TeacherName,A.TeacherId AS TeacherId
FROM            Course AS c LEFT JOIN 
                         Semester ON c.SemesterId = Semester.SemesterId LEFT JOIN AssignCourseToTeacher AS a ON C.CourseId=A.CourseId LEFT JOIN Teacher ON a.TeacherId=Teacher.TeacherId WHERE  c.DepartmentId=" + departmentId;
            Command = new SqlCommand(query, Connection);

            Connection.Open();
            Reader = Command.ExecuteReader();
            List<CourseStatisticsViewModel> courseStatisticsViewModels = new List<CourseStatisticsViewModel>();

            while (Reader.Read())
            {
                CourseStatisticsViewModel courseStatistics = new CourseStatisticsViewModel();
                courseStatistics.DepartmentId = Convert.ToInt32(Reader["DepartmentId"]);
                courseStatistics.CourseCode = (Reader["CourseCode"]).ToString();
                courseStatistics.CourseName = (Reader["CourseName"]).ToString();
                courseStatistics.SemesterName = (Reader["SemesterName"]).ToString();
                if (Reader["TeacherName"].ToString() != "")
                {
                    courseStatistics.TeacherName = Reader["TeacherName"].ToString();
                }
                else 
                {
                    courseStatistics.TeacherName = "Not Assigned Yet";
                }

                

                //courseStatistics.TeacherName = (Reader["TeacherName"]).ToString();
                courseStatisticsViewModels.Add(courseStatistics);

            }
            Reader.Close();
            Connection.Close();
            return courseStatisticsViewModels;

        }

          public List<Course> GetCourseByDepartmentId(int departmentId)
          {
              string query = "SELECT * FROM Course WHERE Status='Assigned' AND DepartmentId=" + departmentId;
            Command = new SqlCommand(query, Connection);
            
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Course> courseList = new List<Course>();

            while (Reader.Read())
            {
                Course course = new Course();
                course.CourseId = Convert.ToInt32(Reader["CourseId"]);
                course.Code = Reader["Code"].ToString();
                course.Name = Reader["Name"].ToString();
                course.Credit = Convert.ToInt32(Reader["Credit"]);
                course.Description = Reader["Description"].ToString();
                course.DepartmentId = Convert.ToInt32(Reader["DepartmentId"]);
                course.SemesterId = Convert.ToInt32(Reader["SemesterId"]);
                course.Status = Reader["Status"].ToString();

                courseList.Add(course);
            }

            Reader.Close();
            Connection.Close();
            return courseList;
        }

          public Course GetCourseByCourseId(int courseId)
          {
              string query = "SELECT * FROM Course WHERE Status='Assigned' AND CourseId=" + courseId;
              Command = new SqlCommand(query, Connection);

              Connection.Open();
              Reader = Command.ExecuteReader();
               Course course=new Course();

              while (Reader.Read())
              {
                 course = new Course();
                  course.CourseId = Convert.ToInt32(Reader["CourseId"]);
                  course.Code = Reader["Code"].ToString();
                  course.Name = Reader["Name"].ToString();
                  course.Credit = Convert.ToInt32(Reader["Credit"]);
                  course.Description = Reader["Description"].ToString();
                  course.DepartmentId = Convert.ToInt32(Reader["DepartmentId"]);
                  course.SemesterId = Convert.ToInt32(Reader["SemesterId"]);
                  course.Status = Reader["Status"].ToString();

                  
              }

              Reader.Close();
              Connection.Close();
              return course;
          }

        public int Save(AssignCourseToTeacher assign)
        {
            string query = "INSERT INTO AssignCourseToTeacher VALUES(@departmentId,@teacherId,@courseId,@status)";
            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@departmentId", assign.DepartmentId);
            Command.Parameters.AddWithValue("@teacherId", assign.TeacherId);
            Command.Parameters.AddWithValue("@courseId", assign.CourseId);
            Command.Parameters.AddWithValue("@status", assign.Status);
            Connection.Open();
            int rowAffect = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffect;

        }

          public bool IsCourseAssigned(AssignCourseToTeacher assign)
          {

              string query = "SELECT * FROM AssignCourseToTeacher WHERE Status='Assigned' AND CourseId=@id ";

            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@id", assign.CourseId);
         

            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExists = Reader.HasRows;
            Connection.Close();
            return isExists;

        } 
        }
    }

   

    
