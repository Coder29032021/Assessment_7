using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using FieldAgent.Core.Interfaces.DAL;
using FieldAgent.Core.Entities;
using FieldAgent.DAL.Repositories;
using System.Threading.Tasks;

namespace FieldAgent.MVC.Controllers
{
    public class AgencyController : Controller
    {
        private IAgencyRepository _agencyRepository;
        
        public AgencyController(IAgencyRepository agencyRepository)
        {
            _agencyRepository = agencyRepository;
        }

        [Route("agency")]
        [HttpGet]
        public IActionResult Index()
        {
            var result = _agencyRepository.GetAll();

            if(result.Success)
            {
                return View(result.Data)
            }
            else
            {
                throw new Exception(result.Message);
            }
        }
    }
}
