using Microsoft.AspNetCore.Mvc;
using WebCourseWorkActual.Domain.Entity;
using WebCourseWorkActual.Domain.ViewModels.CheckExpenseCategory;
using WebCourseWorkActual.Domain.ViewModels.CheckIncomeCategory;
using WebCourseWorkActual.Domain.ViewModels.ExpeseCategory;
using WebCourseWorkActual.Domain.ViewModels.IncomeCategory;
using WebCourseWorkActual.Service.Interfaces;

namespace WebCourseWorkActual.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IExpenseCategoryService _expenseCategoryService;
        private readonly ICheckExpenseCategoryService _checkExpenseCategoryService;
        private readonly IIncomeCategoryService _incomeCategoryService;
        private readonly ICheckIncomeCategoryService _checkIncomeCategoryService;

        public CategoriesController(IExpenseCategoryService expenseCategoryService, ICheckExpenseCategoryService checkExpenseCategoryService, IIncomeCategoryService incomeCategoryService, ICheckIncomeCategoryService checkIncomeCategoryService)
        {
            _expenseCategoryService = expenseCategoryService;
            _checkExpenseCategoryService = checkExpenseCategoryService;
            _incomeCategoryService = incomeCategoryService;
            _checkIncomeCategoryService = checkIncomeCategoryService;
        }

        [HttpGet]
        public IActionResult AddNewExpenseCategory() => View();

        [HttpPost]
        public async Task<IActionResult> AddNewExpenseCategory(NewExpenseCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _expenseCategoryService.Create(model, int.Parse(User.Identity.Name));
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    var response2 = await _checkExpenseCategoryService.Create(new CheckExpenseCategoryViewModel { IdСчёта = int.Parse(User.Identity.Name), IdКатегорииРасхода = response.Data.Id });
                    if (response2.StatusCode == Domain.Enum.StatusCode.OK)
                        return RedirectToAction("GetCategories", "Categories");
                    ModelState.AddModelError("", response2.Description);
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var expenseResponse = await _expenseCategoryService.GetExpenseCategories(int.Parse(User.Identity.Name));
            var incomeResponse = await _incomeCategoryService.GetIncomeCategories(int.Parse(User.Identity.Name));

            General info = new General();
            info.incomeCategories = incomeResponse.Data.ToList();
            info.expenseCategories = expenseResponse.Data.ToList();

            return View(info);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteExpenseCategory(string name)
        {
            
            var response = await _expenseCategoryService.DeleteExpenseCategory(name, int.Parse(User.Identity.Name));
            return View();
        }

        [HttpGet]
        public IActionResult ChangeExpenseCategory(string oldName) 
        {
            ChangeExpenseCategoryViewModel model = new ChangeExpenseCategoryViewModel { Name = oldName };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeExpenseCategory(ChangeExpenseCategoryViewModel model, string oldName)
        {
            if (ModelState.IsValid)
            {
                var response = await _expenseCategoryService.ChangeExpenseCategory(model, int.Parse(User.Identity.Name), oldName);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("GetCategories", "Categories");
                    //var response2 = await _expenseCategoryService.Update(new CheckExpenseCategoryViewModel { IdСчёта = int.Parse(User.Identity.Name), IdКатегорииРасхода = response.Data.Id });
                    //if (response2.StatusCode == Domain.Enum.StatusCode.OK)
                    //    return RedirectToAction("GetExpenseCategories", "ExpenseCategory");
                    ModelState.AddModelError("", response.Description);
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
            //var response = await _expenseCategoryService.ChangeExpenseCategory(name, int.Parse(User.Identity.Name));
            //return View();
        }

        [HttpGet]
        public IActionResult AddNewIncomeCategory() => View();

        [HttpPost]
        public async Task<IActionResult> AddNewIncomeCategory(NewIncomeCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _incomeCategoryService.Create(model, int.Parse(User.Identity.Name));
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    var response2 = await _checkIncomeCategoryService.Create(new CheckIncomeCategoryViewModel { IdСчёта = int.Parse(User.Identity.Name), IdКатегорииДохода = response.Data.Id });
                    if (response2.StatusCode == Domain.Enum.StatusCode.OK)
                        return RedirectToAction("GetCategories", "Categories");
                    ModelState.AddModelError("", response2.Description);
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetIncomeCategories()
        {
            var response = await _incomeCategoryService.GetIncomeCategories(int.Parse(User.Identity.Name));
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteIncomeCategory(string name)
        {

            var response = await _incomeCategoryService.DeleteIncomeCategory(name, int.Parse(User.Identity.Name));
            return View();
        }

        [HttpGet]
        public IActionResult ChangeIncomeCategory(string oldName)
        {
            ChangeIncomeCategoryViewModel model = new ChangeIncomeCategoryViewModel { Name = oldName };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeIncomeCategory(ChangeIncomeCategoryViewModel model, string oldName)
        {
            if (ModelState.IsValid)
            {
                var response = await _incomeCategoryService.ChangeIncomeCategory(model, int.Parse(User.Identity.Name), oldName);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("GetCategories", "Categories");
                    //var response2 = await _incomeCategoryService.Update(new CheckIncomeCategoryViewModel { IdСчёта = int.Parse(User.Identity.Name), IdКатегорииРасхода = response.Data.Id });
                    //if (response2.StatusCode == Domain.Enum.StatusCode.OK)
                    //    return RedirectToAction("GetIncomeCategories", "IncomeCategory");
                    ModelState.AddModelError("", response.Description);
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
            //var response = await _incomeCategoryService.ChangeIncomeCategory(name, int.Parse(User.Identity.Name));
            //return View();
        }
    }
}
