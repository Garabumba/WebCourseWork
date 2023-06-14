using Microsoft.AspNetCore.Mvc;
using WebCourseWorkActual.Domain.Entity;
using WebCourseWorkActual.Service.Interfaces;

namespace WebCourseWorkActual.Controllers
{
    public class StatisticController : Controller
    {
        private readonly ICheckService _checkService;
        private readonly IExpensesService _expensesService;
        private readonly IIncomesService _incomesService;
        private readonly IExpenseCategoryService _expenseCategoryService;
        private readonly IIncomeCategoryService _incomeCategoryService;

        public StatisticController(ICheckService checkService, IExpensesService expensesService, IIncomesService incomesService, IExpenseCategoryService expenseCategoryService, IIncomeCategoryService incomeCategoryService)
        {
            _checkService = checkService;
            _expensesService = expensesService;
            _incomesService = incomesService;
            _expenseCategoryService = expenseCategoryService;
            _incomeCategoryService = incomeCategoryService;
        }

        [HttpGet]
        //месяц и год нам нужно
        public async Task<IActionResult> GetStatistic(int month, int year)
        {
            //int month = DateTime.Now.Month;
            //int year = DateTime.Now.Year;
            if (User.Identity.IsAuthenticated)
            {
                var currentCheck = await _checkService.GetCheck(int.Parse(User.Identity.Name));
                var expenses = await _expensesService.GetExpenses(int.Parse(User.Identity.Name));
                var expensesCategories = await _expenseCategoryService.GetExpenseCategories(int.Parse(User.Identity.Name));
                List<ExpenseCategory> currentExpenseCategories = new List<ExpenseCategory>();
                General info = new General();
                info.Check = currentCheck;
                info.Expenses = expenses.Data.Where(x => x.Дата.Month == month && x.Дата.Year == year).ToList();
                info.ExpenseCategories = expensesCategories.Data.ToList();
                foreach (var currentExpenseCategoryId in info.Expenses)
                    foreach (var expensesCategoryId in info.ExpenseCategories)
                        if (currentExpenseCategoryId.Id == expensesCategoryId.Id)
                            currentExpenseCategories.Add(expensesCategoryId);

                var incomes = await _incomesService.GetIncomes(int.Parse(User.Identity.Name));
                var incomesCategories = await _incomeCategoryService.GetIncomeCategories(int.Parse(User.Identity.Name));
                List<IncomeCategory> currentIncomeCategories = new List<IncomeCategory>();
                info.Incomes = incomes.Data.Where(x => x.Дата.Month == month && x.Дата.Year == year).ToList();
                info.IncomeCategories = incomesCategories.Data.ToList();
                foreach (var currentIncomeCategoryId in info.Incomes)
                    foreach (var incomesCategoryId in info.IncomeCategories)
                        if (currentIncomeCategoryId.Id == incomesCategoryId.Id)
                            currentIncomeCategories.Add(incomesCategoryId);

                return View(info);
            }
            else
                return View();
        }

        //[HttpPost]
        ////месяц и год нам нужно
        //public async Task<IActionResult> GetStatistic(int month, int year)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var currentCheck = await _checkService.GetCheck(int.Parse(User.Identity.Name));
        //        var expenses = await _expensesService.GetExpenses(int.Parse(User.Identity.Name));
        //        var expensesCategories = await _expenseCategoryService.GetExpenseCategories(int.Parse(User.Identity.Name));
        //        List<ExpenseCategory> currentExpenseCategories = new List<ExpenseCategory>();
        //        General info = new General();
        //        info.Check = currentCheck;
        //        info.Expenses = expenses.Data.Where(x => x.Дата.Month == month && x.Дата.Year == year).ToList();
        //        info.ExpenseCategories = expensesCategories.Data.ToList();
        //        foreach (var currentExpenseCategoryId in info.Expenses)
        //            foreach (var expensesCategoryId in info.ExpenseCategories)
        //                if (currentExpenseCategoryId.Id == expensesCategoryId.Id)
        //                    currentExpenseCategories.Add(expensesCategoryId);

        //        info.ExpenseCategories = currentExpenseCategories;

        //        var incomes = await _incomesService.GetIncomes(int.Parse(User.Identity.Name));
        //        var incomesCategories = await _incomeCategoryService.GetIncomeCategories(int.Parse(User.Identity.Name));
        //        List<IncomeCategory> currentIncomeCategories = new List<IncomeCategory>();
        //        info.Incomes = incomes.Data.Where(x => x.Дата.Month == month && x.Дата.Year == year).ToList();
        //        info.IncomeCategories = incomesCategories.Data.ToList();
        //        foreach (var currentIncomeCategoryId in info.Incomes)
        //            foreach (var incomesCategoryId in info.IncomeCategories)
        //                if (currentIncomeCategoryId.Id == incomesCategoryId.Id)
        //                    currentIncomeCategories.Add(incomesCategoryId);

        //        info.IncomeCategories = currentIncomeCategories;

        //        return View(info);
        //    }
        //    else
        //        return View();
        //}
    }
}