using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace UniversitySystemTechBenders.Gateway
{
    public class BaseGateway
    {
        public SqlConnection Connection { get; set; } 
        public SqlCommand Command { get; set; }
        public SqlDataReader Reader { get; set; }

        public SqlCommand Command1 { get; set; }

        public SqlCommand Command2 { get; set; }

        public SqlCommand Command3 { get; set; }
        public BaseGateway()
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["universityDBConnectionString"].ConnectionString;
            Connection=new SqlConnection(connectionString);
        }
    }

   
}