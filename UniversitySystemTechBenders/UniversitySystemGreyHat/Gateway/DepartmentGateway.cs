using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Gateway
{
    public class DepartmentGateway:BaseGateway
    {
        public int Save(Department department)
        {
            string query = "INSERT INTO Department VALUES(@code,@name)";
            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@code", department.DepartmentCode);
            Command.Parameters.AddWithValue("@name", department.DepartmentName);

            Connection.Open();
            int rowAffect = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffect;


        }
        public bool IsDepartmentExists(Department department)
        {

            string query = "SELECT * FROM Department WHERE DepartmentCode=@code AND DepartmentName=@name ";

            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@code", department.DepartmentCode);
            Command.Parameters.AddWithValue("@name", department.DepartmentName);

            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExists = Reader.HasRows;
            Connection.Close();
            return isExists;

        }
        public bool IsCodeExists(Department department)
        {

            string query = "SELECT * FROM Department WHERE DepartmentCode=@code ";

            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@code", department.DepartmentCode);


            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExists = Reader.HasRows;
            Connection.Close();
            return isExists;

        }
        public bool IsNameExists(Department department)
        {

            string query = "SELECT * FROM Department WHERE  DepartmentName=@name ";

            Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@name", department.DepartmentName);

            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExists = Reader.HasRows;
            Connection.Close();
            return isExists;

        }

        //public bool IsExistsDepartmentNameExists(Department department)
        //{


        //    //string query = "SELECT * FROM Department WHERE DepartmentCode='" + DepartmentName + "'";
        //    string query = "SELECT * FROM Department WHERE DepartmentCode=@name";

        //    Command = new SqlCommand(query, Connection);
        //    Command.Parameters.AddWithValue("@name", department.DepartmentName);

        //    Connection.Open();
        //    Reader = Command.ExecuteReader();
        //    bool isExists = Reader.HasRows;
        //    Connection.Close();
        //    return isExists;

        //}

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
    }
}