using axy.Models.Entities;

using DataAccessLayer.Adapters.Category;
using DataAccessLayer.Entities;

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
            var receipt = ReceiptAdapter.GetReceipt();
           
            return View(receipt);
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
