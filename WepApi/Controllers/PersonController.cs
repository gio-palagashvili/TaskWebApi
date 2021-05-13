using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetPerson(string id)
        {
            var person = new GetPersonRep().GetPersonRepp(id);
            return (new GetPersonRep().GetPersonRepp(id) is null) ? NotFound("Id was not found in the database") : Ok(new GetPersonRep().GetPersonRepp(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(string id)
        {
            _ = new DeletePersonRep(id);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> InsertPerson([FromBody]Person person)
        {
            return Ok("Person Inserted");
        } 
    }
}
