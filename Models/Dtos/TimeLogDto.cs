using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClockPunch.Models.Dtos
{
    public class TimeLogDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public System.DateTime LogDate { get; set; }
        public int Hours { get; set; }
    }
}