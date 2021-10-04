
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


using DataAccessLayer.Adapters.Category;
using DataAccessLayer.Entities;
using axy.Models;

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
            bool isInCome = false;
            var receipt = ReceiptAdapter.GetReceipt();
            var expenditure = ExpenditureAdapter.GetExpenditure();
            if(isInCome)
            {
                ViewBag.Categories = new SelectList(expenditure, "Id", "Name");
            }
            else
            {
                ViewBag.Categories = new SelectList(receipt, "Id", "Name");
            }
           
            return View();
        }

        [HttpPost]
        public IActionResult Index(ModelVueHome model)
        {
            var category = new CategoryDto();
            if(model.IsIncome==true)
            {
                var expenditure = new ExpenditureDto();
                var expenditureList = ExpenditureAdapter.GetExpenditure();
                string nameCategory = expenditureList.Where(z => z.Id == model.Id).Select(z=>z.Name).FirstOrDefault();

                expenditure.Name = nameCategory;
                expenditure.Sum = model.Sum;              
                var ExpenditureId =  ExpenditureAdapter.SaveExpenditure(expenditure);


                category.Id = model.Id;
                category.NameCategory = nameCategory;
                category.DescriptionCategory = model.Description;
                category.CurrentDate = model.CurrentData;
                category.IsIncome = true;
                category.ExpenditureId = ExpenditureId;
                CategoryAdapter.SaveCategory(category);              
               
                
            }
            else
            {
                var receipt = new ReceiptDto();
                var receiptList = ReceiptAdapter.GetReceipt();
                string nameCategory = receiptList.Where(z => z.Id == model.Id).Select(z => z.Name).FirstOrDefault();

                receipt.Name = nameCategory;
                receipt.Sum = model.Sum;
                var receiptId = ReceiptAdapter.SaveReceipt(receipt);

                category.Id = model.Id;
                category.NameCategory = nameCategory;
                category.DescriptionCategory = model.Description;
                category.CurrentDate = model.CurrentData;
                category.IsIncome = false;
                category.ReceiptId = receiptId;
                CategoryAdapter.SaveCategory(category);
            }


            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categoryList = CategoryAdapter.GetCategory();
            var filter = categoryList.Where(z => !string.IsNullOrEmpty(z.NameCategory)).ToList();
            return View(filter);
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
            var receipt = ReceiptAdapter.GetReceipt();
            var expenditure = ExpenditureAdapter.GetExpenditure();          

            var listCost = new RecieprsExpenditure();
            listCost.GetReceipts = receipt;
            listCost.GetExpenditures = expenditure;

            return View(listCost);
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

        [HttpGet]
        public ViewResult EditReceipts(int id)
        {

            var model = ReceiptAdapter.GetReceiptDtoId(id);

            return View(model);
        }


        [HttpPost]
        public IActionResult EditReceipts(ReceiptDto model)
        {
            if (ModelState.IsValid)
            {
                ReceiptAdapter.SaveReceipt(model);
                TempData["message"] = $"{model} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }


        [HttpGet]
        public IActionResult DeleteReceipts(int Id)
        {
            if (Id > 0)
            {
                ReceiptAdapter.DeleteReceipt(Id);
                return RedirectToAction("Index");
            }          
           
            return NotFound();
        }

        [HttpGet]
        public ViewResult EditExpenditures(int id)
        {
            var res = ExpenditureAdapter.GetExpenditureDtoId(id);
           
            return View(res);
        }

        [HttpPost]
        public IActionResult EditExpenditures(ExpenditureDto model)
        {
            if (ModelState.IsValid)
            {
                ExpenditureAdapter.SaveExpenditure(model);
               
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult DeleteExpenditure(int Id)
        {
            if (Id > 0)
            {
                ExpenditureAdapter.DeleteExpenditure(Id);
                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}
