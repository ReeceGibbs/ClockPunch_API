using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClockPunch.Models.Dtos
{
    public class TimeLogsDayByDayRsDto
    {
        public string ClientName { get; set; }
        public string ProjectName { get; set; }
        public string EmployeeName { get; set; }
        public DateTime LogDate { get; set; }
        public int Hours { get; set; }
    }
}