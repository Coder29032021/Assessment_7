using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.MVC.Models;
using FieldAgent.Core;
using FieldAgent.Core.Interfaces.DAL;
using FieldAgent.Core.DTOs;
using FieldAgent.Core.Entities;
using FieldAgent.DAL.Repositories;
using System.Threading.Tasks;

namespace FieldAgent.MVC.Controllers
{
    public class ReportsController : Controller
    {
        private IReportsRepository _repoRepository;

        public ReportsController(IReportsRepository repoRepository)
        {
            _repoRepository = repoRepository;
        }

        [Route("Reports")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [Route("Reports/TopAgent")]
        [HttpGet]
        public IActionResult TopAgent()
        {
            var data = _repoRepository.GetTopAgents();

            return View(data.Data);
        }
        //All Good Above

        //generate method to retrieve values 
        [Route("Reports/Pensions")]
        [HttpGet]
        public IActionResult Pensions()
        {
            PensionId pensionId = new PensionId();

            return View(pensionId);
        }
        [Route("Reports/ListPensions")]
        [HttpGet]
        public IActionResult ListPensions(PensionId pension)
        {
            List<PensionListItem> response = (_repoRepository.GetPensionList(pension.agencyId).Data);
            return View(response);
        }
        [Route("Reports/ClearanceId")]
        [HttpGet]
        public IActionResult ClearanceId()
        {
            Clearance clearance = new Clearance();

            return View(clearance);
        }
        [Route("Reports/ListClearance")]
        [HttpGet]
        public IActionResult ListClearance(Clearance clearance)
        {
            List<ClearanceAuditListItem> response = (_repoRepository.AuditClearance(clearance.securityId, clearance.agencyId).Data);
            return View(response);
        }
    }
}
