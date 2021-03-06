USE [master]
GO
/****** Object:  Database [UniversityDB_TechBenders]    Script Date: 12/20/2018 10:37:19 PM ******/
CREATE DATABASE [UniversityDB_TechBenders]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'UniversityDB_TechBenders', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\UniversityDB_TechBenders.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'UniversityDB_TechBenders_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\UniversityDB_TechBenders_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [UniversityDB_TechBenders] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [UniversityDB_TechBenders].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [UniversityDB_TechBenders] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET ARITHABORT OFF 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET  DISABLE_BROKER 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET  MULTI_USER 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [UniversityDB_TechBenders] SET DB_CHAINING OFF 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [UniversityDB_TechBenders] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [UniversityDB_TechBenders]
GO
/****** Object:  StoredProcedure [dbo].[RemainingCredit]    Script Date: 12/20/2018 10:37:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RemainingCredit]
@teacherId int
AS

 BEGIN
SELECT * ,(SELECT SUM(TCR)-(SUM(RCR)) RemainingCredit FROM (
SELECT SUM(TakenCredit) TCR,0 RCR  FROM Teacher WHERE Teacher.TeacherId=X.TeacherId
UNION
SELECT 0 TCR,SUM(Credit) RCR  FROM Course WHERE  Course.CourseId=X.CourseId

)A) AS RemainingCredit FROM (


SELECT   DISTINCT     Teacher.TeacherId AS TeacherId, Course.CourseId AS CourseId, AssignCourseToTeacher.DepartmentId, Course.Credit, Teacher.TakenCredit
FROM            Teacher INNER JOIN
                         AssignCourseToTeacher ON Teacher.TeacherId = AssignCourseToTeacher.TeacherId INNER JOIN
                         Course ON AssignCourseToTeacher.CourseId = Course.CourseId WHERE Teacher.TeacherId=@teacherId)X 
						 END

						


GO
/****** Object:  Table [dbo].[AssignCourseToTeacher]    Script Date: 12/20/2018 10:37:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AssignCourseToTeacher](
	[AssignCourseToTeacherId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[TeacherId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[Status] [varchar](50) NULL,
 CONSTRAINT [PK_AssignCourseToTeacherInfo] PRIMARY KEY CLUSTERED 
(
	[AssignCourseToTeacherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_AssignCourseToTeacherInfo] UNIQUE NONCLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ClassRoomAllocation]    Script Date: 12/20/2018 10:37:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ClassRoomAllocation](
	[ClassRoomAllocationId] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NOT NULL,
	[RoomId] [int] NOT NULL,
	[DayId] [int] NOT NULL,
	[FromTime] [varchar](50) NOT NULL,
	[ToTime] [varchar](50) NOT NULL,
	[Status] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ClassRoomAllocationInfo] PRIMARY KEY CLUSTERED 
(
	[ClassRoomAllocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Course]    Script Date: 12/20/2018 10:37:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Course](
	[CourseId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Credit] [decimal](18, 2) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[DepartmentId] [int] NOT NULL,
	[SemesterId] [int] NOT NULL,
	[Status] [varchar](50) NULL,
 CONSTRAINT [PK_CourseInfo] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CourseInfo] UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CourseInfo_1] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Day]    Script Date: 12/20/2018 10:37:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Day](
	[DayId] [int] IDENTITY(1,1) NOT NULL,
	[DayName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_DayInfo] PRIMARY KEY CLUSTERED 
(
	[DayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_DayInfo] UNIQUE NONCLUSTERED 
(
	[DayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Department]    Script Date: 12/20/2018 10:37:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentCode] [varchar](50) NOT NULL,
	[DepartmentName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_DepartmentInfo] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_DepartmentInfo] UNIQUE NONCLUSTERED 
(
	[DepartmentCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_DepartmentInfo_1] UNIQUE NONCLUSTERED 
(
	[DepartmentName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Designation]    Script Date: 12/20/2018 10:37:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Designation](
	[DesignationId] [int] IDENTITY(1,1) NOT NULL,
	[DesignationName] [varchar](50) NULL,
 CONSTRAINT [PK_Designation] PRIMARY KEY CLUSTERED 
(
	[DesignationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Designation] UNIQUE NONCLUSTERED 
(
	[DesignationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EnrollCourse]    Script Date: 12/20/2018 10:37:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EnrollCourse](
	[EnrollCourseId] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[Date] [varchar](50) NOT NULL,
	[Status] [varchar](50) NULL,
 CONSTRAINT [PK_EnrollCourseInfo] PRIMARY KEY CLUSTERED 
(
	[EnrollCourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GradeLetter]    Script Date: 12/20/2018 10:37:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GradeLetter](
	[GradeLetterId] [int] IDENTITY(1,1) NOT NULL,
	[GradeLetterName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_GradeLetterInfo] PRIMARY KEY CLUSTERED 
(
	[GradeLetterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_GradeLetterInfo] UNIQUE NONCLUSTERED 
(
	[GradeLetterName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Login]    Script Date: 12/20/2018 10:37:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Login](
	[LoginId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[LoginId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mytable]    Script Date: 12/20/2018 10:37:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mytable](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NULL,
	[Unit] [varchar](10) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Room]    Script Date: 12/20/2018 10:37:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Room](
	[RoomId] [int] IDENTITY(1,1) NOT NULL,
	[RoomNo] [varchar](50) NOT NULL,
	[Status] [varchar](50) NULL,
 CONSTRAINT [PK_RoomInfo] PRIMARY KEY CLUSTERED 
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_RoomInfo] UNIQUE NONCLUSTERED 
(
	[RoomNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Semester]    Script Date: 12/20/2018 10:37:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Semester](
	[SemesterId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_SemesterInfo] PRIMARY KEY CLUSTERED 
(
	[SemesterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_SemesterInfo] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Student]    Script Date: 12/20/2018 10:37:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Student](
	[StudentId] [int] IDENTITY(1,1) NOT NULL,
	[RegistrationNo] [nvarchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[ContactNo] [varchar](50) NOT NULL,
	[Date] [varchar](50) NOT NULL,
	[Address] [varchar](max) NOT NULL,
	[DepartmentId] [int] NOT NULL,
 CONSTRAINT [PK_StudentInfo_1] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_StudentInfo] UNIQUE NONCLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_StudentInfo_1] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_StudentInfo_2] UNIQUE NONCLUSTERED 
(
	[ContactNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StudentResult]    Script Date: 12/20/2018 10:37:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentResult](
	[StudentResultId] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[GradeId] [int] NOT NULL,
 CONSTRAINT [PK_StudentResultInfo] PRIMARY KEY CLUSTERED 
(
	[StudentResultId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 12/20/2018 10:37:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Teacher](
	[TeacherId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Address] [varchar](max) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[ContactNumber] [varchar](50) NOT NULL,
	[DesignationId] [int] NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[TakenCredit] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_TeacherInfo] PRIMARY KEY CLUSTERED 
(
	[TeacherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_TeacherInfo] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_TeacherInfo_1] UNIQUE NONCLUSTERED 
(
	[ContactNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Student]    Script Date: 12/20/2018 10:37:19 PM ******/
CREATE NONCLUSTERED INDEX [IX_Student] ON [dbo].[Student]
(
	[RegistrationNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AssignCourseToTeacher]  WITH CHECK ADD  CONSTRAINT [FK_AssignCourseToTeacherInfo_CourseInfo] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[AssignCourseToTeacher] CHECK CONSTRAINT [FK_AssignCourseToTeacherInfo_CourseInfo]
GO
ALTER TABLE [dbo].[AssignCourseToTeacher]  WITH CHECK ADD  CONSTRAINT [FK_AssignCourseToTeacherInfo_DepartmentInfo] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([DepartmentId])
GO
ALTER TABLE [dbo].[AssignCourseToTeacher] CHECK CONSTRAINT [FK_AssignCourseToTeacherInfo_DepartmentInfo]
GO
ALTER TABLE [dbo].[AssignCourseToTeacher]  WITH CHECK ADD  CONSTRAINT [FK_AssignCourseToTeacherInfo_TeacherInfo] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Teacher] ([TeacherId])
GO
ALTER TABLE [dbo].[AssignCourseToTeacher] CHECK CONSTRAINT [FK_AssignCourseToTeacherInfo_TeacherInfo]
GO
ALTER TABLE [dbo].[ClassRoomAllocation]  WITH CHECK ADD  CONSTRAINT [FK_ClassRoomAllocationInfo_CourseInfo] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[ClassRoomAllocation] CHECK CONSTRAINT [FK_ClassRoomAllocationInfo_CourseInfo]
GO
ALTER TABLE [dbo].[ClassRoomAllocation]  WITH CHECK ADD  CONSTRAINT [FK_ClassRoomAllocationInfo_DayInfo] FOREIGN KEY([DayId])
REFERENCES [dbo].[Day] ([DayId])
GO
ALTER TABLE [dbo].[ClassRoomAllocation] CHECK CONSTRAINT [FK_ClassRoomAllocationInfo_DayInfo]
GO
ALTER TABLE [dbo].[ClassRoomAllocation]  WITH CHECK ADD  CONSTRAINT [FK_ClassRoomAllocationInfo_RoomInfo] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([RoomId])
GO
ALTER TABLE [dbo].[ClassRoomAllocation] CHECK CONSTRAINT [FK_ClassRoomAllocationInfo_RoomInfo]
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_CourseInfo_DepartmentInfo] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([DepartmentId])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_CourseInfo_DepartmentInfo]
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_CourseInfo_SemesterInfo] FOREIGN KEY([SemesterId])
REFERENCES [dbo].[Semester] ([SemesterId])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_CourseInfo_SemesterInfo]
GO
ALTER TABLE [dbo].[EnrollCourse]  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseInfo_CourseInfo] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[EnrollCourse] CHECK CONSTRAINT [FK_EnrollCourseInfo_CourseInfo]
GO
ALTER TABLE [dbo].[EnrollCourse]  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseInfo_StudentInfo] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([StudentId])
GO
ALTER TABLE [dbo].[EnrollCourse] CHECK CONSTRAINT [FK_EnrollCourseInfo_StudentInfo]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_StudentInfo_DepartmentInfo] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([DepartmentId])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_StudentInfo_DepartmentInfo]
GO
ALTER TABLE [dbo].[StudentResult]  WITH CHECK ADD  CONSTRAINT [FK_StudentResultInfo_GradeLetterInfo] FOREIGN KEY([GradeId])
REFERENCES [dbo].[GradeLetter] ([GradeLetterId])
GO
ALTER TABLE [dbo].[StudentResult] CHECK CONSTRAINT [FK_StudentResultInfo_GradeLetterInfo]
GO
ALTER TABLE [dbo].[StudentResult]  WITH CHECK ADD  CONSTRAINT [FK_StudentResultInfo_StudentInfo] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([StudentId])
GO
ALTER TABLE [dbo].[StudentResult] CHECK CONSTRAINT [FK_StudentResultInfo_StudentInfo]
GO
ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD  CONSTRAINT [FK_TeacherInfo_DepartmentInfo] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([DepartmentId])
GO
ALTER TABLE [dbo].[Teacher] CHECK CONSTRAINT [FK_TeacherInfo_DepartmentInfo]
GO
ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD  CONSTRAINT [FK_TeacherInfo_DesignationInfo] FOREIGN KEY([DesignationId])
REFERENCES [dbo].[Designation] ([DesignationId])
GO
ALTER TABLE [dbo].[Teacher] CHECK CONSTRAINT [FK_TeacherInfo_DesignationInfo]
GO
USE [master]
GO
ALTER DATABASE [UniversityDB_TechBenders] SET  READ_WRITE 
GO
