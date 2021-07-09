using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ClockPunch.Models.Dtos;
using ClockPunchDataAccess;

namespace ClockPunch.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    public class EmployeesController : ApiController
    {

        //a method to get a list of all employees
        [HttpGet]
        public IEnumerable<EmployeeDto> GetEmployeeList()
        {

            //we reference our entity model
            using (ClockPunchEntities entities = new ClockPunchEntities())
            {

                //we grab all employees from the data context
                List<Employee> employees = entities.Employees.ToList();

                //we create a list of employee dtos that we will return 
                List<EmployeeDto> employeeDtos = new List<EmployeeDto>();

                //we iterate through the list of employees
                foreach (Employee employee in employees)
                {

                    //we add a new employeedto for each employee that exists
                    employeeDtos.Add(new EmployeeDto()
                    {
                        Id = employee.Id,
                        EmployeeName = employee.EmployeeName
                    });
                }

                //we return the list of employee dtos
                return employeeDtos;
            }
        }

        //a method to get a employee item by id
        [HttpGet]
        public EmployeeDto GetEmployee(int id)
        {

            //we reference our entity model
            using (ClockPunchEntities entities = new ClockPunchEntities())
            {

                //we grab our employee from the data context
                Employee employee = entities.Employees.FirstOrDefault(e => e.Id == id);

                //we define a new employee dto with the values returned
                EmployeeDto employeeDto = new EmployeeDto()
                {
                    Id = employee.Id,
                    EmployeeName = employee.EmployeeName
                };

                //we return the employee dto
                return employeeDto;
            }
        }

        //a method used to create employees
        [HttpPost]
        public IHttpActionResult CreateEmployee(EmployeeDto employee)
        {

            //we validate the model sent to this method
            if (!ModelState.IsValid)
            {

                //if the model is not valid, we return a bad request
                return BadRequest("Employee model invalid.");
            }

            //we reference our model entity to run the insert
            using (ClockPunchEntities entities = new ClockPunchEntities())
            {

                //we add the new employee to the entity reference
                entities.Employees.Add(new Employee()
                {
                    EmployeeName = employee.EmployeeName
                });

                //we save the changes so they persist in the database
                entities.SaveChanges();
            }

            //we return a successful response
            return Ok("Employee successfully created.");
        }

        //a method that can be used to update a employee
        [HttpPut]
        public IHttpActionResult UpdateEmployee(EmployeeDto employee)
        {

            //we validate the model sent to this method
            if (!ModelState.IsValid)
            {

                //if the model is not valid, we return a bad request
                return BadRequest("Employee model invalid.");
            }

            //we reference our model entity to run the update
            using (ClockPunchEntities entities = new ClockPunchEntities())
            {

                //we reference the employee item
                Employee dbEmployee = entities.Employees.FirstOrDefault(e => e.Id == employee.Id);

                //if the dbEmployee is null, we return a bad request
                if (dbEmployee == null)
                {

                    return BadRequest("Invalid employee Id.");
                }

                //we update the values of the employee item in the data context
                dbEmployee.EmployeeName = employee.EmployeeName;

                //we save the changes so they persist in the database
                entities.SaveChanges();
            }

            //we return a successful response
            return Ok("Employee successfully updated.");
        }

        //a method that can be called to delete a employee item
        [HttpDelete]
        public IHttpActionResult DeleteEmployee(int id)
        {

            //we reference our model entity to run the delete
            using (ClockPunchEntities entities = new ClockPunchEntities())
            {

                //we remove the item from the dbContext
                entities.Employees.Remove(entities.Employees.FirstOrDefault(e => e.Id == id));

                //we save the changes so they persist in the database
                entities.SaveChanges();
            }

            //we return a successful response
            return Ok("Employee successfully deleted.");
        }
    }
}