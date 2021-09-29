using axy.Models.Entities;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace axy.Controllers
{
    public class ExpendituresController : Controller
    {
        public IActionResult Index()
        {
            var expenditure = new List<ExpenditureDto>();
            expenditure.Add(new ExpenditureDto() { Id = 1, Name = "Freelance", Sum = 1000 });
            expenditure.Add(new ExpenditureDto() { Id = 2, Name = "Work to company", Sum = 1000 });
            return View(expenditure);
        }

        [HttpGet]
        public ViewResult Edit(int id)
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

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return Redirect("/Home/Categories");
        }
    }
}
