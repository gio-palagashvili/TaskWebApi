using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskWeb.Repository;
using TaskWebApi;
using Microsoft.AspNetCore.Mvc.Localization;
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
        [ProducesResponseType(200, Type = typeof(List<PersonRelations>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<PersonRelations>>> GetPersonRelations(string id)
        {
            var relations = await PersonRelationship.GetRelations(id);
            return relations.Count > 0 
                ? Ok(relations) 
                : NotFound("user relationship was not found");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeletePersonRelationsAll(string id)
        {
            return PersonRelationship.DeleteRelationsAll(id).Result.ErrorCode == ErrorList.OK 
                ? Ok("Relations Deleted") 
                : NotFound("User Doesn't have relations");
        }

        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteSingleRelation([FromBody]SingleRelation relation)
        {
            return PersonRelationship.DeleteRelation(relation).Result.ErrorCode == ErrorList.OK
                ? Ok("Relation Deleted")
                : NotFound("relation was not found");
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateRelation(SingleRelation relation)
        {
            var z = await PersonRelationship.CreateRelation(relation);
            return z.ErrorCode == ErrorList.OK 
                ? Ok("Relation created") 
                : BadRequest(z.Description);
        }

        [HttpPost("/RelationReport/{id}/{type}")]
        [ProducesResponseType(200, Type = typeof(List<PersonRelations>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<PersonRelations>>> RelationReport(string id, string type)
        {
            var z = await PersonRelationship.RelationReportRep(id, type);
            return z.Count > 0 
                ? Ok(z) 
                : NotFound(new ErrorClass{ErrorCode = ErrorList.ERROR_NON_EXISTENT,Description = $"no relation was found error : {ErrorList.ERROR_NON_EXISTENT}"});
        }
    }
}