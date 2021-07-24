using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystemTechBenders.Gateway;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Manager
{
    public class RoomManager
    {
        private RoomGateway roomGateway;

        public RoomManager()
        {
            roomGateway=new RoomGateway();
        }
        public List<Room> GetAllRooms()
        {
            return roomGateway.GetAllRooms();
        }
        public List<SelectListItem> GetRoomsForDropDown()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            selectListItems.Add(new SelectListItem() { Text = "--Select--", Value = "" });
            foreach (Room room in GetAllRooms())
            {
                SelectListItem selectList = new SelectListItem();
                selectList.Text = room.RoomNo;
                selectList.Value = room.RoomId.ToString();
                selectListItems.Add(selectList);

            }
            return selectListItems;
        }
    }
}