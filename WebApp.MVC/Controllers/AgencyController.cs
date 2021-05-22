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
                return View(result.Data);
            }
            else
            {
                throw new Exception(result.Messages[0]);
            }
        }
        [Route("agency/add")]
        [HttpGet]
        public IActionResult Add()
        {
            var model = new Agency();
            return View(model);
        }
        [Route("agency/add/{agency}")]
        [HttpPost]
        public IActionResult Add(Agency agency)
        {
            var result = _agencyRepository.Insert(agency);

            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                throw new Exception(result.Messages[0]);
            }
        }
        [Route("agency/edit/{id}")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = _agencyRepository.Get(id);

            if (result.Success)
            {
                return View(result.Data);
            }
            else
            {
                throw new Exception(result.Messages[0]);
            }
        }

        [Route("agency/edit/{id}")]
        [HttpPost]
        public IActionResult Edit(Agency model)
        {
            var result = _agencyRepository.Update(model);

            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                // todo: add validation messages to form later
                throw new Exception(result.Messages[0]);
            }
        }
        [Route("agency/remove/{id}")]
        [HttpGet]
        public IActionResult Remove(int id)
        {
            var result = _agencyRepository.Get(id);

            if (result.Success)
            {
                return View(result.Data);
            }
            else
            {
                throw new Exception(result.Messages[0]);
            }
        }

        [Route("agency/remove/{id}")]
        [HttpPost]
        public IActionResult Remove(Agency model)
        {
            var result = _agencyRepository.Delete(model.AgencyId);

            if (result.Success)
            {
                return RedirectToAction("List");
            }
            else
            {
                throw new Exception(result.Messages[0]);
            }
        }
    }
}
