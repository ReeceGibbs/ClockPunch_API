using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClockPunch.Models.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public int ClientId { get; set; }
    }
}