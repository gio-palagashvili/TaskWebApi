using System;
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
            if (Convert.ToInt32(result) == 0) _ = new InsertPersonRep(person);

            return result switch
            {
                PersonVerify.ErrorList.FirstNameLength => BadRequest("First Name was not in the correct format(no numbers and more than 2 chars)"),
                PersonVerify.ErrorList.NamesDupe => BadRequest("Duplicate Name"),
                PersonVerify.ErrorList.LastNameLength => BadRequest("Last Name was not in the correct format"),
                PersonVerify.ErrorList.CityContainsNumbers => BadRequest("city can not contain numbers"),
                PersonVerify.ErrorList.PrivateNumberLength => BadRequest("private number must be 11 digits"),
                PersonVerify.ErrorList.BinaryGender => BadRequest("must be either 0 or 1"),
                PersonVerify.ErrorList.PhoneNumberDupe => BadRequest("phone number dupe"),
                PersonVerify.ErrorList.InvalidFormatDate => BadRequest("date was not in the correct format"),
                PersonVerify.ErrorList.UnderAged => BadRequest("must be over 18"),
                _ => Ok("person inserted")
            };
        } 
    }
}
