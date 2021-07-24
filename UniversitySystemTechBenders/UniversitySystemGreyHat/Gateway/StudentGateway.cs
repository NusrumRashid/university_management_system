using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UniversityManagementWebApp.Models;
using UniversityManagementWebApp.Models.ViewModel;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Gateway
{
    public class StudentGateway:BaseGateway
    {
        private StudentModel studentModel;
        private StudentViewModel studentViewModel;
        public StudentGateway()
        {
            studentModel=new StudentModel();
            studentViewModel=new StudentViewModel();
        }

        public static DateTime TimeZone(DateTime datetime)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(datetime, timeZoneInfo);
            return PrintDate;
        }
        public StudentModel IdGenerate(string department,int departmentId)
        {

            string ID = "";
            string date = TimeZone(DateTime.Now).ToString("yyyy");
           Connection.Open();
           Command = new SqlCommand(@"SELECT Max(SUBSTRING(RegistrationNo,10,3)) FROM Student Where DepartmentId=" + departmentId, Connection);
           Reader = Command.ExecuteReader();
            if (Reader.Read())
            {

                ID = Reader[0].ToString();
                if (ID == "")
                {
                    ID = department + "-" + date + "-" + "001";
                }
                else
                {
                    ID = (int.Parse(ID) + 1).ToString();
                    if (int.Parse(ID) < 10)
                        ID = department + "-" + date + "-" + "00" + ID;
                    else if (int.Parse(ID) < 100)
                    {
                        ID = department + "-" + date + "-" + "0" + ID;
                    }
                    
                    else
                    {
                        ID = ID;
                    }
                }
            }
           Connection.Close();

            Reader.Close();
            studentModel.RegistrationNo =  ID;
            return studentModel;
        }

        public List<StudentViewModel> GetAllDepartments()
        {
            string query = "SELECT * FROM Department ";

            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();


            List<StudentViewModel> departmentList = new List<StudentViewModel>();
            while (Reader.Read())
            {
                StudentViewModel department = new StudentViewModel();

                department.DepartmentId = Convert.ToInt32(Reader["DepartmentId"]);
                department.DepartmentCode = Reader["DepartmentCode"].ToString();
                department.DepartmentName = Reader["DepartmentName"].ToString();


                departmentList.Add(department);
            }
            Connection.Close();
            return departmentList;

        }



        public int Save(StudentModel studentModel)
        {
            string query = "INSERT INTO Student VALUES(@RegistrationNo, @Name, @Email, @ContactNo, @Date, @Address, @DepartmentId)";
            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@RegistrationNo", studentModel.RegistrationNo);
            Command.Parameters.AddWithValue("@Name", studentModel.Name);
            Command.Parameters.AddWithValue("@Email", studentModel.Email);
            Command.Parameters.AddWithValue("@ContactNo", studentModel.ContactNo);
            Command.Parameters.AddWithValue("@Date", studentModel.Date);
            Command.Parameters.AddWithValue("@Address", studentModel.Address);
            Command.Parameters.AddWithValue("@DepartmentId", studentModel.DepartmentId);
           

            Connection.Open();
            int rowAffect = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffect;


        }
        public bool IsRegistrationExist(StudentModel studentModel)
        {

            string query = "SELECT * FROM Student WHERE RegistrationNo=@RegistrationNo";

            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@RegistrationNo", studentModel.RegistrationNo);

            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExists = Reader.HasRows;
            Connection.Close();
            return isExists;

        }public bool IsEmailExist(StudentModel studentModel)
        {

            string query = "SELECT * FROM Student WHERE Email=@Email";

            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@Email", studentModel.Email);

            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExists = Reader.HasRows;
            Connection.Close();
            return isExists;

        }public bool IsContactExist(StudentModel studentModel)
        {

            string query = "SELECT * FROM Student WHERE ContactNo=@ContactNo";

            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@ContactNo", studentModel.ContactNo);

            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExists = Reader.HasRows;
            Connection.Close();
            return isExists;

        }

        public List<StudentViewModel> GetStudentInfo(StudentModel studentModel)
        {
            string query = @"SELECT        Student.RegistrationNo AS RegistrationNo, Student.StudentId AS StudentId, Student.Name AS Name, Student.Email AS Email, Student.ContactNo AS ContactNo, Student.Date AS Date, Student.Address AS Address, Student.DepartmentId AS DepartmentId, Department.DepartmentCode AS DepartmentCode
FROM            Student INNER JOIN
                         Department ON Student.DepartmentId = Department.DepartmentId WHERE Student.RegistrationNo=(SUBSTRING('" + studentModel.RegistrationNo + "',1,14)) ";

            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();


            List<StudentViewModel> StudentInfoModel = new List<StudentViewModel>();
            while (Reader.Read())
            {
                StudentViewModel StudentInfo = new StudentViewModel();

                StudentInfo.DepartmentId = Convert.ToInt32(Reader["DepartmentId"]);
                StudentInfo.DepartmentCode = Reader["DepartmentCode"].ToString();
                StudentInfo.RegistrationNo = Reader["RegistrationNo"].ToString();
                StudentInfo.StudentId = Convert.ToInt32(Reader["StudentId"]);

                StudentInfo.Name = Reader["Name"].ToString();
                StudentInfo.Email = Reader["Email"].ToString();
                StudentInfo.ContactNo = Reader["ContactNo"].ToString();
                StudentInfo.Date = Reader["Date"].ToString();
                StudentInfo.Address = Reader["Address"].ToString();
              


                StudentInfoModel.Add(StudentInfo);
            }
            Connection.Close();
            return StudentInfoModel;

        }
    }
}