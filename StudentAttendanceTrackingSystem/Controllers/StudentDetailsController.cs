using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuisnessLayer;


namespace StudentAttendanceTrackingSystem.Controllers
{
    public class StudentDetailsController : Controller
    {

        public ActionResult Home()
        {
            return View();
        }
        
      [HttpGet]
      [ActionName("Record")]
        public ActionResult Record_Get()
        {
            ViewBag.Message = TempData["Message"];
            StudentBusinessLayer studentBuisnessLayer=new StudentBusinessLayer() ;
            List<Student> students = studentBuisnessLayer.Students.ToList();
            return View(students);
        }

      [HttpPost]
      [ActionName("Record")]
      public ActionResult Record_Post(List<Student> students, string datepicker)
       {
          DateTime selDate = DateTime.ParseExact(datepicker, "MM/dd/yyyy", null);

          TryUpdateModel(students);
          if (ModelState.IsValid)
          {
              StudentBusinessLayer studentBuisnessLayer = new StudentBusinessLayer();
              string msg=studentBuisnessLayer.UpdateAttendance(students,selDate);
              TempData["Message"] = msg;
          }
          return RedirectToAction("Record");
      }

      [HttpGet]
      [ActionName("TrackByDate")]
      public ActionResult Track_Get()
      {
          return View();
      }

      [HttpPost]
      [ActionName("TrackByDate")]
      public ActionResult Track_Post(string datepicker)
      {
          ViewBag.Message = "No records found for the selected date";
          DateTime selDate = DateTime.ParseExact(datepicker, "MM/dd/yyyy", null);
          StudentBusinessLayer studentBuisnessLayer = new StudentBusinessLayer();
          List<Student> students = studentBuisnessLayer.StudentsAttendance(selDate).ToList();
          return View(students);
          
      }

      [HttpGet]
      [ActionName("TrackByStudent")]
      public ActionResult TrackByStud_Get()
      {
          StudentBusinessLayer studentBuisnessLayer = new StudentBusinessLayer();
          List<Student> students = studentBuisnessLayer.Students.ToList();
          ViewBag.studentList = new SelectList(students, "StudentId", "StudentName"); 
          return View();
      }

      [HttpPost]
      [ActionName("TrackByStudent")]
      public ActionResult TrackByStud_Post(int studentList)
      {
          StudentBusinessLayer studentBuisnessLayer = new StudentBusinessLayer();
          List<Student> students = studentBuisnessLayer.Students.ToList();
          ViewBag.studentList = new SelectList(students, "StudentId", "StudentName");
          List<Attendnace> attendanceList = studentBuisnessLayer.GetAttendanceByStudent(studentList).ToList();
          return View(attendanceList);
      }
    }
}