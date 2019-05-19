using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STM.Models.Data;
using STM.Services;

namespace STM.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private STM_DBContext db { get; set; }
        private ICryptography crypto { get; set; }

        public ProjectController(STM_DBContext db, ICryptography crypto)
        {
            this.db = db;
            this.crypto = crypto;
        }

        public IActionResult Index()
        {
            var projects = db.CProject.ToList();
            return View(projects);
        }

        public IActionResult Project(int id)
        {
            db.CProject.Where(p => p.Id == id).FirstOrDefault();

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult Create(CProject proj)
        {

            return RedirectToAction("Project", proj);
        }
    }
}