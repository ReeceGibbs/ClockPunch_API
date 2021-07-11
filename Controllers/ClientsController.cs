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
    public class ClientsController : ApiController
    {
        
        //a method to get a list of all clients
        [HttpGet]
        public IEnumerable<ClientDto> GetClientList()
        {

            //we reference our entity model
            using (ClockPunchEntities entities = new ClockPunchEntities())
            {

                //we grab all clients from the data context
                List<Client> clients = entities.Clients.ToList();

                //we create a list of client dtos that we will return 
                List<ClientDto> clientDtos = new List<ClientDto>();

                //we iterate through the list of clients
                foreach (Client client in clients)
                {

                    //we add a new clientdto for each client that exists
                    clientDtos.Add(new ClientDto() 
                    { 
                        Id = client.Id,
                        ClientName = client.ClientName
                    });
                }

                //we return the list of client dtos
                return clientDtos;
            }
        }

        //a method to get a client item by id
        [HttpGet]
        public ClientDto GetClient(int id)
        {

            //we reference our entity model
            using (ClockPunchEntities entities = new ClockPunchEntities())
            {

                //we grab our client from the data context
                Client client = entities.Clients.FirstOrDefault(c => c.Id == id);

                //we define a new client dto with the values returned
                ClientDto clientDto = new ClientDto()
                {
                    Id = client.Id,
                    ClientName = client.ClientName
                };

                //we return the client dto
                return clientDto;
            }
        }

        //a method used to create clients
        [HttpPost]
        public IHttpActionResult CreateClient(ClientDto client)
        {

            //we validate the model sent to this method
            if (!ModelState.IsValid)
            {

                //if the model is not valid, we return a bad request
                return BadRequest("Client model invalid.");
            }

            //we reference our model entity to run the insert
            using (ClockPunchEntities entities = new ClockPunchEntities())
            {

                //we add the new client to the entity reference
                entities.Clients.Add(new Client()
                {
                    ClientName = client.ClientName
                });

                //we save the changes so they persist in the database
                entities.SaveChanges();
            }

            //we return a successful response
            return Ok("Client successfully created.");
        }

        //a method that can be used to update a client
        [HttpPut]
        public IHttpActionResult UpdateClient(ClientDto client)
        {

            //we validate the model sent to this method
            if (!ModelState.IsValid)
            {

                //if the model is not valid, we return a bad request
                return BadRequest("Client model invalid.");
            }

            //we reference our model entity to run the update
            using (ClockPunchEntities entities = new ClockPunchEntities())
            {

                //we reference the client item
                Client dbClient = entities.Clients.FirstOrDefault(c => c.Id == client.Id);

                //if the dbClient is null, we return a bad request
                if (dbClient == null)
                {

                    return BadRequest("Invalid client Id.");
                }

                //we update the values of the client item in the data context
                dbClient.ClientName = client.ClientName;

                //we save the changes so they persist in the database
                entities.SaveChanges();
            }

            //we return a successful response
            return Ok("Client successfully updated.");
        }

        //a method that can be called to delete a client item
        [HttpDelete] 
        public IHttpActionResult DeleteClient(int id)
        {

            //we reference our model entity to run the delete
            using (ClockPunchEntities entities = new ClockPunchEntities())
            {

                //we remove the item from the dbContext
                entities.Clients.Remove(entities.Clients.FirstOrDefault(c => c.Id == id));

                //we save the changes so they persist in the database
                entities.SaveChanges();
            }

            //we return a successful response
            return Ok("Client successfully deleted.");
        }
    }
}