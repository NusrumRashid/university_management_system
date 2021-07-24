using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversitySystemTechBenders.Gateway;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Manager
{
    public class DepartmentManager
    {
        private DepartmentGateway departmentGateway;

        public DepartmentManager()
        {
            departmentGateway = new DepartmentGateway();
        }

        public string Save(Department department)
        {

            if (departmentGateway.IsDepartmentExists(department))
            {
                return "Department Code or Name Already Exists";
            }
            else if (departmentGateway.IsCodeExists(department))
            {
                return "Department Code Already Exists";
                
            }
            else if (departmentGateway.IsNameExists(department))
            {
                return "Department Name Already Exists";
                
            }
            else

            {
                int rowAffect = departmentGateway.Save(department);
                if (rowAffect > 0)
                {
                    return "Save Successful";
                }
                else
                {
                    return "Save Failed";
                }
            }


        }

        public List<Department> GetAllDepartments()
        {
            return departmentGateway.GetAllDepartments();
        }
    }
}
