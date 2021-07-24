using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Gateway
{
    public class GradeLetterGateway:BaseGateway
    {
        public List<GradeLetter> GetAllGradeLetters()
        {
            string query = "SELECT * FROM GradeLetter";

            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();


            List<GradeLetter> gradeLetterList = new List<GradeLetter>();
            while (Reader.Read())
            {
                GradeLetter gradeLetter = new GradeLetter();

                gradeLetter.GradeLetterId = Convert.ToInt32(Reader["GradeLetterId"]);
                gradeLetter.GradeLetterName = Reader["GradeLetterName"].ToString();


                gradeLetterList.Add(gradeLetter);
            }
            Connection.Close();
            return gradeLetterList;

        }
    }
}