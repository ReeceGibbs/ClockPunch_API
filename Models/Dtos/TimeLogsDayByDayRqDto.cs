using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClockPunch.Models.Dtos
{
    public class TimeLogsDayByDayRqDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}