using axy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Collections;
using DataAccessLayer.EF.Models;
using DataAccessLayer.Entities;
using axy.Models.Entities;

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
            var category = CategoryAdapter.GetCategory();
           
            var modelView = new GetModelView();
            modelView.GetCategories = category;
          
            
            ViewBag.Categories = new SelectList(category, "Id", "Name");
            return View(modelView);
        }

        [HttpPost]
        public IActionResult Index(ModelVueHome model)
        {
            var cateory = new CategoryDto();            
            cateory.Name = model.Name;
            cateory.Description = model.Description;
            cateory.Cost = model.Cost;
            cateory.CurrentDate = model.CurrentData.ToString();
            cateory.Income = model.Income;
            cateory.IsIncome = model.IsIncome;
       
           
            CategoryAdapter.SaveCategory(cateory);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            var recipt = new List<ReceiptDto>();
            recipt.Add(new ReceiptDto() { Id = 1, Name = "Product", Sum = 12.3m });
            recipt.Add(new ReceiptDto() { Id = 2, Name = "Relax", Sum = 36.7m });
            return View(recipt);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public ViewResult Categories(int Id)
        {
            return View("Categories");
        }

        [HttpPost]
        public IActionResult Categories(RecieprsExpenditure model)
        {
            if (ModelState.IsValid)
            {
                //repository.SaveProduct(product);
                TempData["message"] = $"{model} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
    }
}
