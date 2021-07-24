using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystemTechBenders.Gateway;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Manager
{
    public class GradeLetterManager
    {
        private GradeLetterGateway gradeLetterGateway;

        public GradeLetterManager()
        {
            gradeLetterGateway=new GradeLetterGateway();
        }

        public List<GradeLetter> GetAllGradeLetters()
        {
            return gradeLetterGateway.GetAllGradeLetters();
        }
        public List<SelectListItem> GetGradesForDropDown()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            selectListItems.Add(new SelectListItem() { Text = "--Select a Grade--", Value = "" });
            foreach (GradeLetter grade in GetAllGradeLetters())
            {
                SelectListItem selectList = new SelectListItem();
                selectList.Text = grade.GradeLetterName;
                selectList.Value = grade.GradeLetterId.ToString();
                selectListItems.Add(selectList);

            }
            return selectListItems;
        }
    }
}