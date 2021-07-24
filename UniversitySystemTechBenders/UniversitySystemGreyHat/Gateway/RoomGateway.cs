using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Gateway
{
    public class RoomGateway:BaseGateway
    {
        public List<Room> GetAllRooms()
        {
            string query = "SELECT * FROM Room WHERE Status='UnAllocated' ";

            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();


            List<Room> roomList = new List<Room>();
            while (Reader.Read())
            {
                Room room = new Room();

                room.RoomId = Convert.ToInt32(Reader["RoomId"]);
                room.RoomNo = Reader["RoomNo"].ToString();
                room.Status = Reader["Status"].ToString();


                roomList.Add(room);
            }
            Connection.Close();
            return roomList;

        }
    }
}