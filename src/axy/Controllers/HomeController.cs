using axy.Models;
using BusinessLogic.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Map;

namespace axy.Controllers
{
   // [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ModelVueHome model)
        {
            var cateory = new CategoryDto();            
            cateory.Name = model.Name;
            cateory.Description = model.Description;
            MapCategory.Map(cateory);

            var prise = new PriceDto();
            prise.CurrentDate = model.CurrentData;
            prise.Cost = model.Price;
            prise.Income = model.Price;
            prise.IsIncome = model.IsIncome;
            MapPrice.Map(prise);
           // var qwe = new SelectList()  add list and drop data on UI
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
