using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using WebCourseWorkActual.DAL.Interfaces;
using WebCourseWorkActual.Domain.Entity;
using WebCourseWorkActual.Domain.ViewModels.Expenses;
using WebCourseWorkActual.Domain.ViewModels.Incomes;
using WebCourseWorkActual.Models;
using WebCourseWorkActual.Service.Interfaces;

namespace WebCourseWorkActual.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICheckService _checkService;
        private readonly IExpensesService _expensesService;
        private readonly IIncomesService _incomesService;
        private readonly IExpenseCategoryService _expenseCategoryService;
        private readonly IIncomeCategoryService _incomeCategoryService;

        public HomeController(ICheckService checkService, IExpensesService expensesService, IIncomesService incomesService, IExpenseCategoryService expenseCategoryService, IIncomeCategoryService incomeCategoryService)
        {
            _checkService = checkService;
            _expensesService = expensesService;
            _incomesService = incomesService;
            _expenseCategoryService = expenseCategoryService;
            _incomeCategoryService = incomeCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var response = await _userRepository.Select();
            if (User.Identity.IsAuthenticated)
            {
                var response = await _checkService.GetCheck(int.Parse(User.Identity.Name));
                var response2 = await _expensesService.GetExpenses(int.Parse(User.Identity.Name));
                var expensesCategories = await _expenseCategoryService.GetExpenseCategories(int.Parse(User.Identity.Name));
                List<ExpenseCategory> currentExpenseCategories = new List<ExpenseCategory>();
                General info = new General();
                info.Check = response;
                info.Expenses = response2.Data.ToList();
                info.ExpenseCategories = expensesCategories.Data.ToList();
                //foreach (var currentExpenseCategoryId in info.Expenses)
                //    foreach (var expensesCategoryId in info.ExpenseCategories)
                //        if (currentExpenseCategoryId.Id == expensesCategoryId.Id)
                //            currentExpenseCategories.Add(expensesCategoryId);

                //info.ExpenseCategories = currentExpenseCategories;

                var response3 = await _incomesService.GetIncomes(int.Parse(User.Identity.Name));
                var incomesCategories = await _incomeCategoryService.GetIncomeCategories(int.Parse(User.Identity.Name));
                List<IncomeCategory> currentIncomeCategories = new List<IncomeCategory>();
                info.Incomes = response3.Data.ToList();
                info.IncomeCategories = incomesCategories.Data.ToList();
                //foreach (var currentIncomeCategoryId in info.Incomes)
                //    foreach (var incomesCategoryId in info.IncomeCategories)
                //        if (currentIncomeCategoryId.Id == incomesCategoryId.Id)
                //            currentIncomeCategories.Add(incomesCategoryId);

                //info.IncomeCategories = currentIncomeCategories;

                return View(info);
            }
            else
                return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddNewExpense()
        {
            NewExpenseViewModel model = new NewExpenseViewModel();
            var checkCategories = await _expenseCategoryService.GetExpenseCategories(int.Parse(User.Identity.Name));
            //List<string> categories = new List<string>();
            //model.Category = t.Data
            //foreach (var categoryName in checkCategories.Data)
            //    categories.Add(categoryName.Название);
            //model.Category = new SelectList(categories, "Value", "Text");
            //model.Category = new SelectList(checkCategories.Data.ToList(), "Название", "Название");
            //model.SelectedCategory = model.SelectedCategory;
            ViewBag.Category = new SelectList(checkCategories.Data.AsEnumerable(), "Название", "Название");
            /*model.Category = t.Data
            .Select(x => new SelectListItem
            {
                Value = x.Название,
                Text = x.Название
            });*/
            //ViewData["Category"] = model.Category;
            
            return View(model);
        }

        private async Task<Check> ChangeBalance(NewExpenseViewModel newModel = null, bool isAdd = false, bool isDelete = false, decimal oldSum = 0, ChangeExpenseViewModel changeModel = null, NewIncomeViewModel newIModel = null, ChangeIncomeViewModel changeIModel = null, bool isIncome = false)
        {
            var actualBalance = await _checkService.GetCheck(int.Parse(User.Identity.Name));
            if (!isIncome)
            {
                if (isAdd)
                    actualBalance.Баланс -= decimal.Parse(newModel.Sum);
                else if (isDelete)
                    actualBalance.Баланс += oldSum;
                else
                {
                    actualBalance.Баланс += oldSum;
                    actualBalance.Баланс -= decimal.Parse(changeModel.Sum);
                }
            }
            else
            {
                if (isAdd)
                    actualBalance.Баланс += decimal.Parse(newIModel.Sum);
                else if (isDelete)
                    actualBalance.Баланс -= oldSum;
                else
                {
                    actualBalance.Баланс -= oldSum;
                    actualBalance.Баланс += decimal.Parse(changeIModel.Sum);
                }
            }
            return await _checkService.ChangeBalance(actualBalance);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewExpense(NewExpenseViewModel model)
        {
            var checkCategories = await _expenseCategoryService.GetExpenseCategories(int.Parse(User.Identity.Name));
            ViewBag.Category = new SelectList(checkCategories.Data.AsEnumerable(), "Название", "Название");
            //model.Category = new SelectList(checkCategories.Data.ToList(), "Название", "Название");
            //model.SelectedCategory = model.SelectedCategory;
            
            if (ModelState.IsValid)
            {
                var response = await _expensesService.Create(model, int.Parse(User.Identity.Name));
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await ChangeBalance(model, true);
                    return RedirectToAction("", "Home");
                }
                ModelState.AddModelError("", response.Description);
            }
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteExpense(int expenseId)
        {
            var deletedExpense = await _expensesService.GetExpenseOnId(expenseId);
            await _expensesService.DeleteExpense(expenseId);
            await ChangeBalance(oldSum: deletedExpense.Data.Сумма, isDelete: true);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ChangeExpense(int expenseId, decimal oldSum)
        {
            var checkCategories = await _expenseCategoryService.GetExpenseCategories(int.Parse(User.Identity.Name));
            var currentExpense = await _expensesService.GetExpenseOnId(expenseId);
            var currentCategory = checkCategories.Data.FirstOrDefault(x => x.Id == currentExpense.Data.IdКатегорииРасхода);

            ViewBag.Category = new SelectList(checkCategories.Data.AsEnumerable(), "Название", "Название");

            var checkExpenses = await _expensesService.GetExpenseOnId(expenseId);

            ChangeExpenseViewModel model = new ChangeExpenseViewModel
            {
                Id = expenseId,
                Name = checkExpenses.Data.Описание,
                Date = checkExpenses.Data.Дата,
                Sum = checkExpenses.Data.Сумма.ToString(),
                Category = currentCategory.Название
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeExpense(ChangeExpenseViewModel model, int expenseId, decimal oldSum)
        {
            if (ModelState.IsValid)
            {
                var response = await _expensesService.ChangeExpense(model, expenseId, int.Parse(User.Identity.Name));
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await ChangeBalance(changeModel: model, oldSum: oldSum);
                    return RedirectToAction("", "Home");
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
        public async Task<IActionResult> AddNewIncome()
        {
            NewIncomeViewModel model = new NewIncomeViewModel();
            var checkCategories = await _incomeCategoryService.GetIncomeCategories(int.Parse(User.Identity.Name));
            //List<string> categories = new List<string>();
            //model.Category = t.Data
            //foreach (var categoryName in checkCategories.Data)
            //    categories.Add(categoryName.Название);
            //model.Category = new SelectList(categories, "Value", "Text");
            //model.Category = new SelectList(checkCategories.Data.ToList(), "Название", "Название");
            //model.SelectedCategory = model.SelectedCategory;
            ViewBag.Category = new SelectList(checkCategories.Data.AsEnumerable(), "Название", "Название");
            /*model.Category = t.Data
            .Select(x => new SelectListItem
            {
                Value = x.Название,
                Text = x.Название
            });*/
            //ViewData["Category"] = model.Category;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewIncome(NewIncomeViewModel model)
        {
            var checkCategories = await _incomeCategoryService.GetIncomeCategories(int.Parse(User.Identity.Name));
            ViewBag.Category = new SelectList(checkCategories.Data.AsEnumerable(), "Название", "Название");
            //model.Category = new SelectList(checkCategories.Data.ToList(), "Название", "Название");
            //model.SelectedCategory = model.SelectedCategory;

            if (ModelState.IsValid)
            {
                var response = await _incomesService.Create(model, int.Parse(User.Identity.Name));
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await ChangeBalance(newIModel: model, isAdd: true, isIncome: true);
                    return RedirectToAction("", "Home");
                }
                ModelState.AddModelError("", response.Description);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteIncome(int incomeId)
        {
            var deletedIncome = await _incomesService.GetIncomeOnId(incomeId);
            await _incomesService.DeleteIncome(incomeId);
            await ChangeBalance(oldSum: deletedIncome.Data.Сумма, isDelete: true, isIncome: true);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ChangeIncome(int incomeId, decimal oldSum)
        {
            var checkCategories = await _incomeCategoryService.GetIncomeCategories(int.Parse(User.Identity.Name));
            var currentIncome = await _incomesService.GetIncomeOnId(incomeId);
            var currentCategory = checkCategories.Data.FirstOrDefault(x => x.Id == currentIncome.Data.IdКатегорииДохода);

            ViewBag.Category = new SelectList(checkCategories.Data.AsEnumerable(), "Название", "Название");

            var checkIncomes = await _incomesService.GetIncomeOnId(incomeId);

            ChangeIncomeViewModel model = new ChangeIncomeViewModel
            {
                Id = incomeId,
                Name = checkIncomes.Data.Описание,
                Date = checkIncomes.Data.Дата,
                Sum = checkIncomes.Data.Сумма.ToString(),
                Category = currentCategory.Название
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeIncome(ChangeIncomeViewModel model, int incomeId, decimal oldSum)
        {
            if (ModelState.IsValid)
            {
                var response = await _incomesService.ChangeIncome(model, incomeId, int.Parse(User.Identity.Name));
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await ChangeBalance(changeIModel: model, oldSum: oldSum, isIncome: true);
                    return RedirectToAction("", "Home");
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