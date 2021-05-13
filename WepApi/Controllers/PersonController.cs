﻿using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TaskWeb.Repository;
using TaskWebApi;

#pragma warning disable 1998

namespace WepApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Person))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Person>> GetPerson(string id)
        {
            var person = new GetPersonRep().GetPersonRepp(id);
            return (new GetPersonRep().GetPersonRepp(id) is null) ? NotFound("Id was not found in the database") : Ok(new GetPersonRep().GetPersonRepp(id));
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePerson(string id)
        {
            return DeletePersonRep.Delete(id).Result ? NotFound($"Person ${id} was not found") : Ok($"Person ${id} was deleted");
        }
        [HttpPost]
        public async Task<IActionResult> InsertPerson([FromBody]Person person)
        {
            return Ok("Person Inserted");
        } 
    }
}