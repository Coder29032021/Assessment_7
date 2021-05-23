using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using FieldAgent.Core.Interfaces.DAL;
using FieldAgent.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.MVC.Controllers
{  
    [Route("api/[controller]")]
    [ApiController]
    public class AliasController : ControllerBase
    {
        private readonly IAliasRepository _aliasRepo;

        public AliasController(IAliasRepository aliasRepo)
        {
            _aliasRepo = aliasRepo;
        }

        [HttpGet]
        [Route("/api/[controller]/{id}")]
        public IActionResult GetAlias(int id)
        {
            var result = _aliasRepo.Get(id);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Messages);
            }
        }
        [HttpGet]
        [Route("/api/[controller]/{id}/ByAgent")]
        public IActionResult GetByAgent(int id)
        {
            var result = _aliasRepo.GetByAgent(id);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Messages);
            }
        }
        [HttpPost]

        public IActionResult AddAlias(Alias alias)
        {
             
            var result = _aliasRepo.Insert(alias);

            if (result.Success)
            {
                return CreatedAtRoute(nameof(GetAlias), new { id = alias.AliasId}, alias);
            }
            else
            {
                return BadRequest(result.Messages);
            }
        }
        [HttpPut]
        public IActionResult EditAlias(Alias alias)
        {

            if (!_aliasRepo.Get(alias.AliasId).Success)
            {
                return NotFound($"Alias {alias.AliasId} not found");
            }

            Alias existingAlias = _aliasRepo.Get(alias.AliasId).Data;
            existingAlias.AgentId = alias.AgentId;
            existingAlias.AliasName = alias.AliasName;
            existingAlias.InterpolId = alias.InterpolId;
            existingAlias.Persona = alias.Persona;

            var result = _aliasRepo.Update(existingAlias);

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Messages);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAlias(int id)
        {
            if (!_aliasRepo.Get(id).Success)
            {
                return NotFound($"Alias {id} not found");
            }

            var result = _aliasRepo.Delete(id);

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Messages);
            }
        }
    }
}
