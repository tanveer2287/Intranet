using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataViewModels;
using Microsoft.AspNetCore.Mvc;
using ePortal.Models;
using Services.Interrface;

namespace WebAppUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeService _empService;

        public HomeController(IEmployeeService empService)
        {
            _empService = empService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _empService.GetEmployees();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveEmployee(EmployeeVM model)
        {
            await _empService.SaveEmployee(model);
            return RedirectToAction("Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Edit(int id)
        {
            ViewData["Message"] = "Your Edit page.";

            return View();
        }

        public IActionResult Delete(int id)
        {
            ViewData["Message"] = "Your Edit page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
