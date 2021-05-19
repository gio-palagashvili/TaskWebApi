using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg;
using TaskWeb.Repository;
using TaskWebApi;
// ReSharper disable CA1822
// ReSharper disable CA1822
#pragma warning disable 1998

namespace WepApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RelationController : Controller
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PersonRelations>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPersonRelations(string id)
        {
            var relations = await PersonRelationship.GetRelations(id);
            return relations.Count > 0 ? Ok(relations) : NotFound("user relationship was not found");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePersonRelationsAll(string id)
        {
            return PersonRelationship.DeleteRelationsAll(id).Result.ErrorCode == ErrorList.OK 
                ? Ok("Relations Deleted") 
                : NotFound("User Doesn't have relations");
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSingleRelation([FromBody]SingleRelation relation)
        {
            return PersonRelationship.DeleteRelation(relation).Result.ErrorCode == ErrorList.OK
                ? Ok("Relation Deleted")
                : NotFound("relation was not found");
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRelation(SingleRelation relation)
        {
            var z = await PersonRelationship.CreateRelation(relation);
            return z.ErrorCode == ErrorList.OK ? Ok("Relation created") : BadRequest(z.Description);
        }
    }
}