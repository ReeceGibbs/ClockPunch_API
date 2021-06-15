using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClockPunch.Models.Dtos;
using ClockPunchDataAccess;

namespace ClockPunch.Controllers
{
    public class TimeLogsController : ApiController
    {

        //a method to get a list of all timeLogs
        [HttpGet]
        public IEnumerable<TimeLogDto> GetTimeLogList()
        {

            //we reference our entity model
            using (ClockPunchEntities entities = new ClockPunchEntities())
            {

                //we grab all timeLogs from the data context
                List<TimeLog> timeLogs = entities.TimeLogs.ToList();

                //we create a list of timeLog dtos that we will return 
                List<TimeLogDto> timeLogDtos = new List<TimeLogDto>();

                //we iterate through the list of timeLogs
                foreach (TimeLog timeLog in timeLogs)
                {

                    //we add a new timeLogdto for each timeLog that exists
                    timeLogDtos.Add(new TimeLogDto()
                    {
                        Id = timeLog.Id,
                        EmployeeName = timeLog.EmployeeName,
                        ClientId = timeLog.ClientId,
                        LogDate = timeLog.LogDate,
                        Hours = timeLog.Hours
                    });
                }

                //we return the list of timeLog dtos
                return timeLogDtos;
            }
        }

        //a method to get a timeLog item by id
        [HttpGet]
        public TimeLogDto GetTimeLog(int id)
        {

            //we reference our entity model
            using (ClockPunchEntities entities = new ClockPunchEntities())
            {

                //we grab our timeLog from the data context
                TimeLog timeLog = entities.TimeLogs.FirstOrDefault(t => t.Id == id);

                ////we define a new timeLog dto with the values returned
                TimeLogDto timeLogDto = new TimeLogDto()
                {
                    Id = timeLog.Id,
                    EmployeeName = timeLog.EmployeeName,
                    ClientId = timeLog.ClientId,
                    LogDate = timeLog.LogDate,
                    Hours = timeLog.Hours
                };

                //we return the timeLog dto
                return timeLogDto;
            }
        }

        //a method used to create timeLogs
        [HttpPost]
        public IHttpActionResult CreateTimeLog(TimeLogDto timeLog)
        {

            //we validate the model sent to this method
            if (!ModelState.IsValid)
            {

                //if the model is not valid, we return a bad request
                return BadRequest("TimeLog model invalid.");
            }

            //we reference our model entity to run the insert
            using (ClockPunchEntities entities = new ClockPunchEntities())
            {

                //we add the new timeLog to the entity reference
                entities.TimeLogs.Add(new TimeLog()
                {
                    EmployeeName = timeLog.EmployeeName,
                    ClientId = timeLog.ClientId,
                    LogDate = timeLog.LogDate,
                    Hours = timeLog.Hours
                });

                //we save the changes so they persist in the database
                entities.SaveChanges();
            }

            //we return a successful response
            return Ok("TimeLog successfully created.");
        }

        //a method that can be used to update a timeLog
        [HttpPut]
        public IHttpActionResult UpdateTimeLog(TimeLogDto timeLog)
        {

            //we validate the model sent to this method
            if (!ModelState.IsValid)
            {

                //if the model is not valid, we return a bad request
                return BadRequest("TimeLog model invalid.");
            }

            //we reference our model entity to run the update
            using (ClockPunchEntities entities = new ClockPunchEntities())
            {

                //we reference the timeLog item
                TimeLog dbTimeLog = entities.TimeLogs.FirstOrDefault(t => t.Id == timeLog.Id);

                //if the dbTimeLog is null, we return a bad request
                if (dbTimeLog == null)
                {

                    return BadRequest("Invalid timeLog Id.");
                }

                //we update the values of the timeLog item in the data context
                dbTimeLog.EmployeeName = timeLog.EmployeeName;
                dbTimeLog.ClientId = timeLog.ClientId;
                dbTimeLog.LogDate = timeLog.LogDate;
                dbTimeLog.Hours = timeLog.Hours;

                //we save the changes so they persist in the database
                entities.SaveChanges();
            }

            //we return a successful response
            return Ok("TimeLog successfully updated.");
        }

        //a method that can be called to delete a timeLog item
        [HttpDelete]
        public IHttpActionResult DeleteTimeLog(int id)
        {

            //we reference our model entity to run the delete
            using (ClockPunchEntities entities = new ClockPunchEntities())
            {

                //we remove the item from the dbContext
                entities.TimeLogs.Remove(entities.TimeLogs.FirstOrDefault(t => t.Id == id));

                //we save the changes so they persist in the database
                entities.SaveChanges();
            }

            //we return a successful response
            return Ok("TimeLog successfully deleted.");
        }
    }
}