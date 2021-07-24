using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystemTechBenders.Gateway;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Manager
{
    public class DayManager
    {
        private DayGateway dayGateway;

        public DayManager()
        {
            dayGateway=new DayGateway();
        }
        public List<Day> GetAllDays()
        {
            return dayGateway.GetAllDays();
        }
        public List<SelectListItem> GetDaysForDropDown()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            selectListItems.Add(new SelectListItem() { Text = "--Select--", Value = "" });
            foreach (Day day in GetAllDays())
            {
                SelectListItem selectList = new SelectListItem();
                selectList.Text = day.DayName;
                selectList.Value = day.DayId.ToString();
                selectListItems.Add(selectList);

            }
            return selectListItems;
        }
    }
}