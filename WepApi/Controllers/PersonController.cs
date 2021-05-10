using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using TaskWeb.Repository;
using TaskWebApi;

namespace WepApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        [HttpGet("{id}")]
        #pragma warning disable 1998
        public async Task<Person> GetPerson(string id) => new GetPersonRep().GetPersonRepp(id);

        [HttpDelete("{id}")]
        public async Task<string> DeletePerson(string id)
        {
            _ = new DeletePersonRep(id);
            return "Successful";
        }
    }
}
