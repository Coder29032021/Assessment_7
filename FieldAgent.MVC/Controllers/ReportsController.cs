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
    public class ReportsController : Controller
    {
        private IReportsRepository _repoRepository;

        public ReportsController(IReportsRepository repoRepository)
        {
            _repoRepository = repoRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
