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
    public class AgentsController : ControllerBase
    {
        private readonly IAgentRepository _agentRepo;

        public AgentsController(IAgentRepository agentRepo)
        {
            _agentRepo = agentRepo;
        }


        [HttpGet]
        [Route("/api/[controller]/{id}",Name ="GetAgent")]
        public IActionResult GetAgent(int id)
        {
            var result = _agentRepo.Get(id);

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
        [Route("/api/[controller]/{id}/GetMissions")]
        public IActionResult GetMissions(int id)
        {
            var result = _agentRepo.GetMissions(id);

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
        [Route("/api/[controller]")]
        public IActionResult AddAgent(Agent agent)
        {

            var result = _agentRepo.Insert(agent);

            if (result.Success)
            {
                return CreatedAtRoute(nameof(GetAgent), new { id = agent.AgentId }, agent);
            }
            else
            {
                return BadRequest(result.Messages);
            }
        }
        [HttpPut]
        public IActionResult EditAgent(Agent agent)
        {

            if (!_agentRepo.Get(agent.AgentId).Success)
            {
                return NotFound($"Alias {agent.AgentId} found");
            }

            Agent existAgent = _agentRepo.Get(agent.AgentId).Data;
            existAgent.FirstName = agent.FirstName;
            existAgent.LastName= agent.LastName;
            existAgent.DateOfBirth= agent.DateOfBirth;
            existAgent.Height= agent.Height;

            var result = _agentRepo.Update(existAgent);

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
            if (!_agentRepo.Get(id).Success)
            {
                return NotFound($"Alias {id} not found");
            }

            var result = _agentRepo.Delete(id);

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
