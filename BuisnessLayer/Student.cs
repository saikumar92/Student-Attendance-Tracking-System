using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BuisnessLayer
{
    public class Student
    {
        public int StudentId { get; set; }
       
        public string StudentName { get; set; }
        [Required]
        public bool AttendanceStatus { get; set; }
    }


}
