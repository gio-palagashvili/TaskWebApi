using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
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
        public async Task<IActionResult> GetPerson(string id)
        {
            if (!await ManagePerson.IdExistAsyncRoute(id)) Ok($"user {id} doesn't exist");
            if (Regex.IsMatch(id, @"[a-zA-Z]")) return BadRequest("id contains letters");
            
            return Ok(await ManagePerson.GetPerson(id));
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePerson(string id)
        {
            return ManagePerson.DeletePerson(id).Result
            ? NotFound($"Person ${id} was not found")
            : Ok($"Person ${id} was deleted");
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertPerson([FromBody] Person person)
        {
            var result = PersonVerify.Verify(person);

            if (result.ErrorCode == ErrorList.OK) _ = ManagePerson.InsertPersonRep(person);

            return result.ErrorCode == ErrorList.OK
            ? Ok("User Inserted")
            : BadRequest(result.Description);
        }

        [HttpGet("search/{value}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Person>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Filter(string value)
        {
            var persons = ManagePerson.FilterPerson(value).Result;
            return persons.Any()
            ? Ok(persons)
            : NotFound("No results");
        }

        [HttpPut("update/UpdateClass>")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdatePersonParameter(UpdateClass update)
        {
            return Ok(await ManagePerson.UpdatePerson(update));
        }
    }
}