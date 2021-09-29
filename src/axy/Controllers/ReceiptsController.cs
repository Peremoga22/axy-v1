using axy.Models.Entities;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace axy.Controllers
{
    public class ReceiptsController : Controller
    {
        public IActionResult Index()
        {
            var recipt = new List<ReceiptDto>();
            recipt.Add(new ReceiptDto() { Id = 1, Name = "Product", Sum = 12.3m });
            recipt.Add(new ReceiptDto() { Id = 2, Name = "Relax", Sum = 36.7m });
            return View(recipt);
        }

        [HttpGet]
        public ViewResult Edit(int Id)
        {
            return View("Edit");
        }

        [HttpPost]
        public IActionResult Edit(ReceiptDto model)
        {
            if (ModelState.IsValid)
            {
                //repository.SaveProduct(product);
                TempData["message"] = $"{model.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
    }
}
