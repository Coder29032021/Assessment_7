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
    public class MissionApiController : ControllerBase
    {
        private readonly IMissionRepository _missRepo;

        public MissionApiController(IMissionRepository missionRepository)
        {
            _missRepo = missionRepository;
        }

    }
}
