using ClockPunch.Models.Dtos;
using ClockPunchDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ClockPunch.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    public class ProjectsController : ApiController
    {

        //a method to get a list of all projects
        [HttpGet]
        public IEnumerable<ProjectDto> GetProjectList()
        {

            //we reference our entity model
            using (ClockPunchEntities entities = new ClockPunchEntities())
            {

                //we grab all projects from the data context
                List<Project> projects = entities.Projects.ToList();

                //we create a list of project dtos that we will return 
                List<ProjectDto> projectDtos = new List<ProjectDto>();

                //we iterate through the list of projects
                foreach (Project project in projects)
                {

                    //we add a new projectdto for each project that exists
                    projectDtos.Add(new ProjectDto()
                    {
                        Id = project.Id,
                        ProjectName = project.ProjectName,
                        ClientId = project.ClientId
                    });
                }

                //we return the list of project dtos
                return projectDtos;
            }
        }

        //a method to get a project item by id
        [HttpGet]
        public ProjectDto GetProject(int id)
        {

            //we reference our entity model
            using (ClockPunchEntities entities = new ClockPunchEntities())
            {

                //we grab our project from the data context
                Project project = entities.Projects.FirstOrDefault(p => p.Id == id);

                //we define a new project dto with the values returned
                ProjectDto projectDto = new ProjectDto()
                {
                    Id = project.Id,
                    ProjectName = project.ProjectName,
                    ClientId = project.ClientId
                };

                //we return the project dto
                return projectDto;
            }
        }

        //a method used to create projects
        [HttpPost]
        public IHttpActionResult CreateProject(ProjectDto project)
        {

            //we validate the model sent to this method
            if (!ModelState.IsValid)
            {

                //if the model is not valid, we return a bad request
                return BadRequest("Project model invalid.");
            }

            //we reference our model entity to run the insert
            using (ClockPunchEntities entities = new ClockPunchEntities())
            {

                //we add the new project to the entity reference
                entities.Projects.Add(new Project()
                {
                    ProjectName = project.ProjectName,
                    ClientId = project.ClientId
                });

                //we save the changes so they persist in the database
                entities.SaveChanges();
            }

            //we return a successful response
            return Ok("Project successfully created.");
        }

        //a method that can be used to update a project
        [HttpPut]
        public IHttpActionResult UpdateProject(ProjectDto project)
        {

            //we validate the model sent to this method
            if (!ModelState.IsValid)
            {

                //if the model is not valid, we return a bad request
                return BadRequest("Project model invalid.");
            }

            //we reference our model entity to run the update
            using (ClockPunchEntities entities = new ClockPunchEntities())
            {

                //we reference the project item
                Project dbProject = entities.Projects.FirstOrDefault(p => p.Id == project.Id);

                //if the dbProject is null, we return a bad request
                if (dbProject == null)
                {

                    return BadRequest("Invalid project Id.");
                }

                //we update the values of the project item in the data context
                dbProject.ProjectName = project.ProjectName;

                dbProject.ClientId = project.ClientId;

                //we save the changes so they persist in the database
                entities.SaveChanges();
            }

            //we return a successful response
            return Ok("Project successfully updated.");
        }

        //a method that can be called to delete a project item
        [HttpDelete]
        public IHttpActionResult DeleteProject(int id)
        {

            //we reference our model entity to run the delete
            using (ClockPunchEntities entities = new ClockPunchEntities())
            {

                //we remove the item from the dbContext
                entities.Projects.Remove(entities.Projects.FirstOrDefault(p => p.Id == id));

                //we save the changes so they persist in the database
                entities.SaveChanges();
            }

            //we return a successful response
            return Ok("Project successfully deleted.");
        }
    }
}
