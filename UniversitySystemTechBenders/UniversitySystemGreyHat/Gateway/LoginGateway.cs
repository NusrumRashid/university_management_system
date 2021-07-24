using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Gateway
{
    public class LoginGateway:BaseGateway
    {
        public bool IsUserValid(Login login)
        {

            string query = "SELECT Password FROM Login WHERE  Username=@username AND Password=@password ";
            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@username", login.Username);
            Command.Parameters.AddWithValue("@password", login.Password);
            
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExists = Reader.HasRows;
            Connection.Close();
            return isExists;
        }
    }
}