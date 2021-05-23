using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using FieldAgent.Core.Interfaces.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.MVC.Controllers
{    //, authorize for all of them --> copy token back in 1x
    //authorization after http action methods on all of them. 
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController : ControllerBase
    {
        private readonly IMissionRepository _missRepo;

        public MissionController(IMissionRepository missionRepository)
        {
            _missRepo = missionRepository;
        }

        [HttpGet]
        [Route("/api/[controller]/{id}", Name = "GetMission")]
        public IActionResult GetMission(int id)
        {
            var result = _missRepo.Get(id);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Messages);
            }
        }
    }
}
