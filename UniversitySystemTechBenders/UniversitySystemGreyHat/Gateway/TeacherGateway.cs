using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Gateway
{
    public class TeacherGateway:BaseGateway
    {
        public int Save(Teacher teacher)
        {
            string query = "INSERT INTO Teacher VALUES(@name,@address,@email,@contactNo,@designationId,@departmentId,@takenCredit)";
            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@name", teacher.Name);
            Command.Parameters.AddWithValue("@address", teacher.Address);
            Command.Parameters.AddWithValue("@email", teacher.Email);
            Command.Parameters.AddWithValue("@contactNo", teacher.ContactNumber);
            Command.Parameters.AddWithValue("@designationId", teacher.DesignationId);
            Command.Parameters.AddWithValue("@departmentId", teacher.DepartmentId);
            Command.Parameters.AddWithValue("@takenCredit", teacher.TakenCredit);

            Connection.Open();
            int rowAffect = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffect;

        }

        public bool IsEmailContactExists(Teacher teacher)
        {

            string query = "SELECT * FROM Teacher WHERE Email=@email AND ContactNumber=@contactNumber AND DepartmentId=@departmentId ";

            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@email", teacher.Email);
            Command.Parameters.AddWithValue("@contactNumber", teacher.ContactNumber);
            Command.Parameters.AddWithValue("@departmentId", teacher.DepartmentId);
           
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExists = Reader.HasRows;

            Connection.Close();
            return isExists;

        } public bool IsEmailExists(Teacher teacher)
        {

            string query = "SELECT * FROM Teacher WHERE Email=@email ";

            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@email", teacher.Email);
           
           
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExists = Reader.HasRows;

            Connection.Close();
            return isExists;

        }public bool IsContactExists(Teacher teacher)
        {

            string query = "SELECT * FROM Teacher WHERE  ContactNumber=@contactNumber";

            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@contactNumber", teacher.ContactNumber);
           
           
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExists = Reader.HasRows;

            Connection.Close();
            return isExists;

        } 
        public int UpdateCredit(Teacher teacher)
        {


            string credit = "UPDATE Teacher SET TakenCredit=@takenCredit WHERE Email=@email AND ContactNumber=@contactNumber ";
                Command = new SqlCommand(credit, Connection);
                Command.Parameters.AddWithValue("@email", teacher.Email);
                Command.Parameters.AddWithValue("@contactNumber", teacher.ContactNumber);
                Command.Parameters.AddWithValue("@takenCredit", teacher.TakenCredit);
                Connection.Open();

                int rowAffected = Command.ExecuteNonQuery();
                
            
            
            Connection.Close();   
            return rowAffected;
        }
       
        public List<Designation> GetAllDesignations()
        {
            string query = "SELECT * FROM Designation ";

            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();


            List<Designation> designationList = new List<Designation>();
            while (Reader.Read())
            {
                Designation designation = new Designation();

                designation.DesignationId = Convert.ToInt32(Reader["DesignationId"]);
                designation.DesignationName = Reader["DesignationName"].ToString();


                designationList.Add(designation);
            }
            Connection.Close();
            return designationList;

        }

    }
}