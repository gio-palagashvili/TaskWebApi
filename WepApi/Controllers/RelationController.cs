﻿using System.Collections.Generic;
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
    [Route("[controller]/{id}")]
    public class RelationController : Controller
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PersonRelations>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPersonRelations(string id)
        {
            var relations = await PersonRelationship.GetRelations(id);
            return relations.Count > 0 ? Ok(relations) : NotFound("user relationship was not found");
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePersonRelationsAll(string id)
        {
            return Ok();
        }
    }
}