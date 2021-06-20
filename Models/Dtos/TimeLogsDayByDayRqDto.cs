using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClockPunch.Models.Dtos
{
    public class TimeLogsDayByDayRqDto
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}