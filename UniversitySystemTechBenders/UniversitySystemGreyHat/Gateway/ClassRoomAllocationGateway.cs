using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversitySystemTechBenders.Models;
using UniversitySystemTechBenders.Models.ViewModel;

namespace UniversitySystemTechBenders.Gateway
{
    public class ClassRoomAllocationGateway:BaseGateway
    {
        public int Save(ClassRoomAllocation allocation)
        {
            string query = "INSERT INTO ClassRoomAllocation VALUES(@courseId,@roomId,@dayId,@fromTime,@toTime,@status)";
            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@courseId", allocation.CourseId);
            Command.Parameters.AddWithValue("@roomId", allocation.RoomId);
            Command.Parameters.AddWithValue("@dayId", allocation.DayId);
            Command.Parameters.AddWithValue("@fromTime", allocation.FromTime);
            Command.Parameters.AddWithValue("@toTime", allocation.ToTime);
            Command.Parameters.AddWithValue("@status", allocation.Status);

            Connection.Open();
            int rowAffect = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffect;

        }

        public List<ClassRoomAllocation> IsTimeScheduleAllocated(ClassRoomAllocation allocation)
        {

            string query = "SELECT * FROM ClassRoomAllocation WHERE  DayId=@dayId AND RoomId=@roomId AND Status='Allocated'";

            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@dayId", allocation.DayId);
            Command.Parameters.AddWithValue("@roomId", allocation.RoomId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<ClassRoomAllocation> classRoomAllocationsList=new List<ClassRoomAllocation>();
            ClassRoomAllocation classRoom = new ClassRoomAllocation();
            if (Reader.Read())
            {
               
                classRoom.FromTime = Reader["FromTime"].ToString();
                classRoom.ToTime = Reader["ToTime"].ToString();
                classRoom.Status = Reader["Status"].ToString();
            }
            classRoomAllocationsList.Add(classRoom);
            Connection.Close();
            return classRoomAllocationsList;
}
       
        public bool IsToTimeSAllocated(ClassRoomAllocation allocation)
        {

            string query = "SELECT * FROM ClassRoomAllocation WHERE  DayId=@dayId AND RoomId=@roomId AND ToTime BETWEEN @fromTime AND @toTime AND Status='Allocated";

            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@dayId", allocation.DayId);
            Command.Parameters.AddWithValue("@roomId", allocation.RoomId);
            Command.Parameters.AddWithValue("@fromTime", allocation.FromTime);
            Command.Parameters.AddWithValue("@toTime", allocation.ToTime);


            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExists = Reader.HasRows;
            Connection.Close();
            return isExists;

        }





        public List<ClassAllocationViewModel> ViewClassScheduleInfoByDepartmentId(int departmentId)
        {
            string query = @"SELECT Code AS CourseCode,CourseId AS CourseId,Name AS CourseName from Course WHERE Status='Assigned' AND DepartmentId=" + departmentId;
            Command = new SqlCommand(query, Connection);

            Connection.Open();
            Reader = Command.ExecuteReader();
            List<ClassAllocationViewModel> classRoomAllocations = new List<ClassAllocationViewModel>();

            while (Reader.Read())
            {
                ClassAllocationViewModel classRoomAllocation = new ClassAllocationViewModel();
                classRoomAllocation.CourseId = Convert.ToInt32(Reader["CourseId"]);
                classRoomAllocation.CourseCode = (Reader["CourseCode"]).ToString();
                classRoomAllocation.CourseName = (Reader["CourseName"]).ToString();

                //if (Reader["RoomId"].ToString() == "" && Reader["DayId"].ToString() == "" && Reader["RoomNo"].ToString() == "" && Reader["DayName"].ToString() == "" && Reader["FromTime"].ToString() == "" && Reader["ToTime"].ToString() == "")
                //{
                //    classRoomAllocation.ScheduleInfo = "Not Allocated Yet";
                //}
                //else if (Reader["RoomId"].ToString() != "" && Reader["DayId"].ToString() != "" && Reader["RoomNo"].ToString() != "" && Reader["DayName"].ToString() != "" && Reader["FromTime"].ToString() != "" && Reader["ToTime"].ToString() != "")
                //{
                //    classRoomAllocation.ScheduleInfo = "Room No :" + Reader["RoomNo"].ToString() + " , " + Reader["DayName"].ToString() + " , " + Reader["FromTime"].ToString() + " - " + Reader["ToTime"].ToString()+";";
                //}
              
                //courseStatistics.TeacherName = (Reader["TeacherName"]).ToString();
                classRoomAllocations.Add(classRoomAllocation);

            }
           
            Connection.Close();
            foreach (ClassAllocationViewModel classRoomAllocation in classRoomAllocations)
            {
                string query1 = @"SELECT        ClassRoomAllocation.FromTime, ClassRoomAllocation.ToTime, Day.DayName, Room.RoomNo
FROM            ClassRoomAllocation INNER JOIN
                         Day ON ClassRoomAllocation.DayId = Day.DayId INNER JOIN
                         Room ON ClassRoomAllocation.RoomId = Room.RoomId WHERE ClassRoomAllocation.Status='Allocated' AND ClassRoomAllocation.CourseId=" + classRoomAllocation.CourseId;
                Command=new SqlCommand(query1,Connection);


                classRoomAllocation.ScheduleInfo = "";

                Connection.Open();

                Reader = Command.ExecuteReader();
                
                while (Reader.Read())
                {
                    classRoomAllocation.ScheduleInfo += "Room No :";
                    classRoomAllocation.ScheduleInfo += Reader["RoomNo"].ToString();
                    classRoomAllocation.ScheduleInfo += " , ";
                    classRoomAllocation.ScheduleInfo += Reader["DayName"].ToString();
                    classRoomAllocation.ScheduleInfo += " , ";
                    classRoomAllocation.ScheduleInfo += Reader["FromTime"].ToString();
                    classRoomAllocation.ScheduleInfo += " - ";
                    classRoomAllocation.ScheduleInfo += Reader["ToTime"].ToString();
                    classRoomAllocation.ScheduleInfo += "; ";
                    classRoomAllocation.ScheduleInfo += "<br/>";
                }
                if (classRoomAllocation.ScheduleInfo=="")
                {
                    classRoomAllocation.ScheduleInfo = "Not Assigned Yet";
                }
                Connection.Close();
            }

           
            Reader.Close();
            return classRoomAllocations;

        }
        public int UnAllocate()
        {
            string query = "Update ClassRoomAllocation SET Status= 'UnAllocated'";

            Command = new SqlCommand(query, Connection);
            string query2 = "Update Room SET Status= 'UnAllocated'";
            Command1 = new SqlCommand(query2, Connection);

            Connection.Open();
            int rowAffect1 = Command.ExecuteNonQuery();
            int rowAffect2 = Command1.ExecuteNonQuery();
            int rowAffect = rowAffect1 + rowAffect2;
            Connection.Close();
            return rowAffect;
        }

        public bool IsRoomUnAllocated()
        {
            string query = "SELECT * FROM ClassRoomAllocation WHERE  Status='UnAllocated'";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExists = Reader.HasRows;
            Connection.Close();
            return isExists;
        }
        public bool IsAllRoomUnAllocated()
        {
            string query = "SELECT * FROM Room WHERE  Status='UnAllocated'";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExists = Reader.HasRows;
            Connection.Close();
            return isExists;
        }
    }
}