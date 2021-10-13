
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
            StateSelectHelperDto model = StateHelperAdapter.GetState();                    
          
            var receipt = ReceiptAdapter.GetReceipt();
            var expenditure = ExpenditureAdapter.GetExpenditure();
            var category = new CategoryDto();
            category.IsIncome = model.IsState;
            var categoryAll = CategoryAdapter.GetCategory();

            var categoryList = CategoryAdapter.GetCategorySum();
            var receiptSum  = categoryList.Select(z => z.BalansRecipt).FirstOrDefault();
            var expenditureSum = categoryList.Select(z => z.BalansExpenditure).FirstOrDefault();
                       
            category.CurrentBalance = receiptSum - expenditureSum;
            category.SavingForThisMounth = receiptSum - expenditureSum;
            category.BalanceTheBeginningMounth = receiptSum;

            ViewData["Costs"] = expenditureSum;                   

            if (model.IsState)
            {
                ViewData["ExpenditureId"] = new SelectList(expenditure, "Id",nameof(ExpenditureDto.Name));               
            }
            else
            {
                ViewData["ReceiptId"] = new SelectList(receipt, "Id", nameof(ReceiptDto.Name));              
            }

             return View(category);          
        }

        [HttpPost]
        public IActionResult Index(CategoryDto model)
        {          
            var category = new CategoryDto();
            var state = StateHelperAdapter.GetState();
            model.IsIncome = state.IsState;
            if (model.IsIncome)
            {
                if (ModelState.IsValid)
                {
                    var expenditureList = ExpenditureAdapter.GetExpenditure();
                    string nameCategory = expenditureList.Where(z => z.Id == model.ExpenditureId).Select(z => z.Name).FirstOrDefault();
                    var expenditureModel = new ExpenditureDto();                    
                    expenditureModel.Id = 0;
                    expenditureModel.Name = nameCategory;
                    expenditureModel.Sum = model.SumExpenditure;                    
                    int expenditureId = ExpenditureAdapter.SaveExpenditure(expenditureModel);

                    category.NameCategory = nameCategory;
                    category.DescriptionCategory = model.DescriptionCategory;
                    category.CurrentDate = model.CurrentDate;
                    category.IsIncome = true;
                    category.ExpenditureId = expenditureId;
                    category.ReceiptId = 0;
                    CategoryAdapter.SaveCategory(category);
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var receiptList = ReceiptAdapter.GetReceipt();
                    string nameCategory = receiptList.Where(z => z.Id == model.ReceiptId).Select(z => z.Name).FirstOrDefault();
                    var recieptModel = new ReceiptDto();
                    recieptModel.Id = 0;
                    recieptModel.Name = nameCategory;
                    recieptModel.Sum = model.SumReceipt;
                    int receiptId = ReceiptAdapter.SaveReceipt(recieptModel);

                                      
                    category.NameCategory = nameCategory;
                    category.DescriptionCategory = model.DescriptionCategory;
                    category.CurrentDate = model.CurrentDate;
                    category.IsIncome = false;
                    category.ReceiptId = receiptId;
                    category.ExpenditureId = 0;
                    CategoryAdapter.SaveCategory(category);
                }
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult IndexSwitcher(bool switcher)
        {
            StateSelectHelperDto model = StateHelperAdapter.GetState();                      
            model.IsState = switcher;
            StateHelperAdapter.Save(model);
            
            return RedirectToAction(nameof(Index));           
        }


        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categorySum = CategoryAdapter.GetCategorySum();
            var expenditureSum = categorySum.Select(z => z.BalansExpenditure).FirstOrDefault();
            ViewData["Costs"] = expenditureSum;
            var categoryList = CategoryAdapter.GetCategory();
            var filter = categoryList.ToList();
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


        [HttpGet]
        public IActionResult Privacy()
        {
            var categorySum = CategoryAdapter.GetCategorySum();
            var expenditureSum = categorySum.Select(z => z.BalansExpenditure).FirstOrDefault();
            ViewData["Costs"] = expenditureSum;
            return View();
        }


        [HttpGet]
        public JsonResult PieChart()
        {          
            CategoryDto model = new CategoryDto();
            List<SumPieDto> sumList = new List<SumPieDto>();
            IEnumerable<CategoryDto> categoryList = new List<CategoryDto>();
            SumPieDto pie = new SumPieDto();

            categoryList = CategoryAdapter.GetCategorySum();
            int count = 1;

            pie.SumExpenditure = categoryList.Select(z => z.SumExpenditure).FirstOrDefault();
            pie.SumReceipt = categoryList.Select(z => z.SumReceipt).FirstOrDefault();

            foreach (var item in categoryList)
            {
                sumList.Add(new SumPieDto() { NameCategory = item.DescriptionCategory, SumReceipt = item.BalansRecipt, SumExpenditure = item.SumExpenditure });

                if (categoryList.Count() == count)
                {
                    var remainderSum = item.BalansRecipt - item.BalansExpenditure;
                    sumList.Add(new SumPieDto() { NameCategory = "Amount on the balance.", SumReceipt = item.BalansRecipt, SumExpenditure = remainderSum });
                    break;
                }

                count++;
            }
            
            return Json(sumList);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public ViewResult Categories()
        {
            var categorySum = CategoryAdapter.GetCategorySum();
            var expenditureSum = categorySum.Select(z => z.BalansExpenditure).FirstOrDefault();
            ViewData["Costs"] = expenditureSum;

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

        [HttpPost]
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
            var categorySum = CategoryAdapter.GetCategorySum();
            var expenditureSum = categorySum.Select(z => z.BalansExpenditure).FirstOrDefault();
            ViewData["Costs"] = expenditureSum;

            var model = ReceiptAdapter.GetReceiptDtoId(id);

            return View(model);
        }


        [HttpPost]
        public IActionResult EditReceipts(ReceiptDto model)
        {
            if (ModelState.IsValid)
            {
                model.Sum = 0;
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
            var categorySum = CategoryAdapter.GetCategorySum();
            var expenditureSum = categorySum.Select(z => z.BalansExpenditure).FirstOrDefault();
            ViewData["Costs"] = expenditureSum;

            var res = ExpenditureAdapter.GetExpenditureDtoId(id);
           
            return View(res);
        }

        [HttpPost]
        public IActionResult EditExpenditures(ExpenditureDto model)
        {
            if (ModelState.IsValid)
            {
                model.Sum = 0;
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
