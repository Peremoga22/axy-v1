﻿
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
            var category = new CategoryDto();
           // category.IsIncome = switcher;
            var categoryAll = CategoryAdapter.GetCategory();

            var categoryList = CategoryAdapter.GetCategorySum();
            var expenditureSum = categoryList.Select(z => z.SumExpenditure).FirstOrDefault();
            var receiptSum = categoryList.Select(z => z.SumReceipt).FirstOrDefault();
            if (expenditureSum > 0)
            {
                category.CurrentBalance = receiptSum - expenditureSum;
                category.SavingForThisMounth = receiptSum - expenditureSum;
                category.BalanceTheBeginningMounth = categoryAll.Where(z=>z.ReceiptId == 1011).Select(z => z.SumReceipt).FirstOrDefault();
            }          

            if (isInCome)
            {
                ViewData["ExpenditureId"] = new SelectList(expenditure, "Id",nameof(ExpenditureDto.Name));
               // return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["ReceiptId"] = new SelectList(receipt, "Id", nameof(ReceiptDto.Name));
              // return RedirectToAction(nameof(Index));
            }

             return View(category);          
        }

        [HttpPost]
        public IActionResult Index(CategoryDto model, bool switcher)
        {
            model.IsIncome = switcher;
            var category = new CategoryDto();
            if(model.IsIncome)
            {
                if (ModelState.IsValid)
                {
                    var expenditureModel = new ExpenditureDto();
                    var expenditureList = ExpenditureAdapter.GetExpenditure();
                    string nameCategory = expenditureList.Where(z => z.Id == model.ExpenditureId).Select(z => z.Name).FirstOrDefault();
                    expenditureModel.Id = (int)model.ExpenditureId;
                    expenditureModel.Name = nameCategory;
                    expenditureModel.Sum = model.SumExpenditure;
                    category.NameCategory = nameCategory;
                    ExpenditureAdapter.SaveExpenditure(expenditureModel);

                    category.DescriptionCategory = model.DescriptionCategory;
                    category.CurrentDate = model.CurrentDate;
                    category.IsIncome = true;
                    category.ExpenditureId = model.ExpenditureId;
                    CategoryAdapter.SaveCategory(category);
                }

            }
            else
            {
                if (ModelState.IsValid)
                {
                    var recieptModel = new ReceiptDto();
                    recieptModel.Id = (int)model.ReceiptId;

                    var receiptList = ReceiptAdapter.GetReceipt();
                    string nameCategory = receiptList.Where(z => z.Id == model.ReceiptId).Select(z => z.Name).FirstOrDefault();
                    recieptModel.Name = nameCategory;
                    recieptModel.Sum = model.SumReceipt;
                    ReceiptAdapter.SaveReceipt(recieptModel);

                    category.ReceiptId = model.Id;
                    category.NameCategory = nameCategory;
                    category.DescriptionCategory = model.DescriptionCategory;
                    category.CurrentDate = model.CurrentDate;
                    category.IsIncome = false;
                    category.ReceiptId = model.ReceiptId;
                    CategoryAdapter.SaveCategory(category);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult IndexSwitcher(bool switcher)
        {
            var receipt = ReceiptAdapter.GetReceipt();
            var expenditure = ExpenditureAdapter.GetExpenditure();
            if(switcher)
            {
                ViewData["ExpenditureId"] = new SelectList(expenditure, "Id", nameof(ExpenditureDto.Name));
            }
            else
            {
                ViewData["ReceiptId"] = new SelectList(receipt, "Id", nameof(ReceiptDto.Name));
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

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var category = CategoryAdapter.GetReceiptDtoId(id);
            var receipt = ReceiptAdapter.GetReceipt();
            var expenditure = ExpenditureAdapter.GetExpenditure();

            if (category.IsIncome == true)
            {
                ViewBag.Categories = new SelectList(expenditure, "Id", "Name");
            }
            else
            {
                ViewBag.Categories = new SelectList(receipt, "Id", "Name");
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult SaveCategory(CategoryDto model)
        {
            model.IsIncome = false;
            var category = new CategoryDto();
            if (model.IsIncome)
            {
                if (ModelState.IsValid)
                {
                    var expenditureModel = new ExpenditureDto();
                    var expenditureList = ExpenditureAdapter.GetExpenditure();
                    var categoryList = CategoryAdapter.GetCategory();
                    var expenditureId = categoryList.Where(z => z.Id == model.Id).Select(z => z.ExpenditureId).FirstOrDefault();
                    string nameCategory = expenditureList.Where(z => z.Id == expenditureId).Select(z => z.Name).FirstOrDefault();
                    var sum = expenditureList.Where(z => z.Id == expenditureId).Select(z => z.Sum).FirstOrDefault();
                    expenditureModel.Id = (int)expenditureId;
                    expenditureModel.Name = nameCategory;
                    expenditureModel.Sum = model.SumExpenditure;                    
                    ExpenditureAdapter.SaveExpenditure(expenditureModel);

                    category.Id = model.Id;
                    category.NameCategory = nameCategory;
                    category.DescriptionCategory = model.DescriptionCategory;
                    category.CurrentDate = model.CurrentDate;
                    category.IsIncome = true;
                    category.ExpenditureId = expenditureId;
                    CategoryAdapter.SaveCategory(category);
                }

            }
            else
            {
                if (ModelState.IsValid)
                {
                    var recieptModel = new ReceiptDto();
                    var recieptList = ReceiptAdapter.GetReceipt();
                    var categoryList = CategoryAdapter.GetCategory();
                    var reciepId = categoryList.Where(z => z.Id == model.Id).Select(z => z.ReceiptId).FirstOrDefault();
                    string nameCategory = categoryList.Where(z => z.Id == model.Id).Select(z => z.NameCategory).FirstOrDefault();
                    recieptModel.Id = (int)reciepId;
                    recieptModel.Name = nameCategory;
                    recieptModel.Sum = model.SumReceipt;
                    ReceiptAdapter.SaveReceipt(recieptModel);

                    category.Id = model.Id;
                    category.NameCategory = nameCategory;
                    category.DescriptionCategory = model.DescriptionCategory;
                    category.CurrentDate = model.CurrentDate;
                    category.IsIncome = false;
                    category.ReceiptId = reciepId;
                    CategoryAdapter.SaveCategory(category);
                }
            }                      

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
        public IActionResult RemoveCategory(int Id)
        {
            if (Id > 0)
            {
                CategoryAdapter.Delete(Id);
                return RedirectToAction("Index");
            }

            return NotFound();
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
