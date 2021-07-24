using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Gateway
{
    public class DayGateway:BaseGateway
    {
        public List<Day> GetAllDays()
        {
            string query = "SELECT * FROM Day order by Dayid";

            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();


            List<Day> dayList = new List<Day>();
            while (Reader.Read())
            {
                Day day = new Day();

                day.DayId = Convert.ToInt32(Reader["DayId"]);
                day.DayName = Reader["DayName"].ToString();


                dayList.Add(day);
            }
            Connection.Close();
            return dayList;

        }
    }
}