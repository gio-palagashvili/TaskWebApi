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
            _ = new GetPersonRep().GetPersonRepp(id);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertPerson([FromBody]Person person)
        {
            var result = PersonVerify.Verify(person);
            
            if (result.ErrorCode == ErrorList.OK) _ = new InsertPersonRep(person);

            return result.ErrorCode == ErrorList.OK ? Ok("User Inserted") : BadRequest(result.Description);
        }
        
        [HttpGet("search/{value}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Person>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Filter(string value)
        {
            var persons = FilterPerson.FilterRep(value).Result;
            return persons.Any() ? Ok(persons) : NotFound();
        }
        
    }
}
