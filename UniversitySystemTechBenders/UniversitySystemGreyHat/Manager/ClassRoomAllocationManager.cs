using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversitySystemTechBenders.Gateway;
using UniversitySystemTechBenders.Models;
using UniversitySystemTechBenders.Models.ViewModel;

namespace UniversitySystemTechBenders.Manager
{
    public class ClassRoomAllocationManager
    {
        private ClassRoomAllocationGateway classRoomAllocationGateway;

        public ClassRoomAllocationManager()
        {
            classRoomAllocationGateway=new ClassRoomAllocationGateway();
        }

        public string Save(ClassRoomAllocation allocation)
        {
            DateTime from = Convert.ToDateTime(allocation.FromTime);
            DateTime to = Convert.ToDateTime(allocation.ToTime);


            List<ClassRoomAllocation> classRoom = classRoomAllocationGateway.IsTimeScheduleAllocated(allocation);
            if (classRoom.Count>0)
            {
                foreach (ClassRoomAllocation allocate in classRoom)
                {
                    DateTime fromTime = Convert.ToDateTime(allocate.FromTime);
                    DateTime toTime = Convert.ToDateTime(allocate.ToTime);
                    string status = allocate.Status;
                    if (from >= fromTime && from < toTime)
                    {
                        return "Room already allocated";
                    }
                    else if (to > fromTime && to <= toTime)
                    {
                        return "Room already allocated";
                    }
                    else if (from <= fromTime && to >= toTime)
                    {

                        return "Room already allocated";
                    }
                else
                    {
                        
                        int rowAffect = classRoomAllocationGateway.Save(allocation);
                        if (rowAffect > 0)
                        {
                            return "Room Allocated successfully";
                        }
                        else
                        {
                            return "Room Allocation failed";

                        }
                    }

                }
                return "Room Allocated successfully";

            }
            else
            {
                int rowAffect = classRoomAllocationGateway.Save(allocation);
                if (rowAffect > 0)
                {
                    return "Room Allocated successfully";
                }
                else
                {
                    return "Room Allocation failed";

                }
            }
        }
    


        public List<ClassAllocationViewModel> ViewClassScheduleInfoByDepartmentId(int departmentId)
        {
            return classRoomAllocationGateway.ViewClassScheduleInfoByDepartmentId(departmentId);
        }
        public string UnAllocate()
        {
            bool isExists = classRoomAllocationGateway.IsRoomUnAllocated();
            bool isExists1 = classRoomAllocationGateway.IsAllRoomUnAllocated();
            if (isExists && isExists1)
            {
                return "Rooms already UnAllocated";
            }
            else
            {
                int rowAffect = classRoomAllocationGateway.UnAllocate();
                if (rowAffect > 0)
                {
                    return "Rooms UnAllocated";
                }
                else
                {
                    return "Allocation Failed";
                }
            }
        }
    }
}