using System.Collections.Generic;
using System.Linq;
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
            return ManagePerson.IdExistAsyncRoute(id).Result ? Ok(ManagePerson.GetPerson(id).Result) : BadRequest("person with that id doesn't exist");
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePerson(string id)
        {
            return ManagePerson.DeletePerson(id).Result ? NotFound($"Person ${id} was not found") : Ok($"Person ${id} was deleted");
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertPerson([FromBody]Person person)
        {
            var result = PersonVerify.Verify(person);
            
            if (result.ErrorCode == ErrorList.OK) _ = ManagePerson.InsertPersonRep(person);

            return result.ErrorCode == ErrorList.OK ? Ok("User Inserted") : BadRequest(result.Description);
        }
        
        [HttpGet("search/{value}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Person>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Filter(string value)
        {
            var persons = ManagePerson.FilterPerson(value).Result;
            return persons.Any() ? Ok(persons) : NotFound();
        }

        [HttpPut("update/{Person}>")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdatePerson(Person person)
        {
            return Ok(ManagePerson.UpdatePerson(person).Result);
        }
        [HttpPut("update/{id}/{type}>")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdatePersonParameter(string type, string id)
        {
            return Accepted();
        }
    }
}