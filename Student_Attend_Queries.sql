--To Creating Database

CREATE DATABASE [Student_Attendance] 



--To Create Students table

CREATE TABLE [dbo].[Students](
	[StudentId] [int] IDENTITY(1,1) NOT NULL,
	[StudentName] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]



-- Inserting data into students table

insert into Students (StudentName) values ('Sherlock Holmes')
insert into Students (StudentName) values ('Joe Watson')
insert into Students (StudentName) values ('Eve Jackson')
insert into Students (StudentName) values ('Jill Smith')
insert into Students (StudentName) values ('Dwayne Johnson')
insert into Students (StudentName) values ('Adam Jampa')



--To create attendance table

CREATE TABLE [dbo].[Attendance](
	[AttendanceId] [int] IDENTITY(1,1) NOT NULL,
	[StudentID] [int] NOT NULL,
	[StudentName] [varchar](100) NULL,
	[AttendanceDate] [date] NOT NULL,
	[AttendanceStatus] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC,
	[AttendanceDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]



-- Get Student Details proc

create procedure [dbo].[usp_GetStudents]
as
begin
select StudentId,StudentName from Students
end



-- Proc to get attendance details by date

create proc [dbo].[usp_GetStudentsAttendance]
(
@attendanceDate date
)
as 
begin
select * from Attendance where AttendanceDate=@attendanceDate
end



-- Proc to update attendance

create procedure [dbo].[usp_UpdateAttendance]
(
@studentId int,
@studentName nvarchar(100),
@attendanceDate date,
@attendanceStatus varchar(10)
)
as
begin
insert into Attendance (StudentId,StudentName,AttendanceDate,AttendanceStatus) 
values(@studentId,@studentName,@attendanceDate,@attendanceStatus)
end



-- Proc to get attendance details by student

create proc [dbo].[usp_GetAttendanceByStudent]
(
@studentId int
)
as 
begin
select * from Attendance where StudentID=@studentId
end
GO
