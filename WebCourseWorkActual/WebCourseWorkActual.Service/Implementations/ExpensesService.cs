using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCourseWorkActual.DAL.Interfaces;
using WebCourseWorkActual.Domain.Entity;
using WebCourseWorkActual.Domain.Enum;
using WebCourseWorkActual.Domain.Response;
using WebCourseWorkActual.Domain.ViewModels.CheckExpenseCategory;
using WebCourseWorkActual.Domain.ViewModels.Expenses;
using WebCourseWorkActual.Domain.ViewModels.ExpeseCategory;
using WebCourseWorkActual.Domain.ViewModels.Exprnses;
using WebCourseWorkActual.Service.Interfaces;

namespace WebCourseWorkActual.Service.Implementations
{
    //ExpenseCategoryService
    public class ExpensesService : IExpensesService
    {
        private readonly ILogger<ExpenseCategoryService> _logger;
        private readonly IBaseRepository<ExpenseCategory> _expenseCategoryRepository;
        private readonly IBaseRepository<CheckExpenseCategory> _checkExpenseCategoryRepository;
        private readonly IBaseRepository<Expenses> _expensesRepository;

        public ExpensesService(ILogger<ExpenseCategoryService> logger, IBaseRepository<Expenses> expenses, IBaseRepository<ExpenseCategory> expenseCategory, IBaseRepository<CheckExpenseCategory> checkExpenseCategory)
        {
            _logger = logger;
            _expensesRepository = expenses;
            _expenseCategoryRepository = expenseCategory;
            _checkExpenseCategoryRepository = checkExpenseCategory;
        }
        public async Task<BaseResponse<IEnumerable<Expenses>>> GetExpenses(int id)
        {
            try
            {
                var expenses = await _expensesRepository.GetAll().Where(x => x.IdСчёта == id).ToListAsync();

                //var checkExpenseCategories = await _checkExpenseCategoryRepository.GetAll().Where(x => x.IdСчёта == id).ToListAsync();
                //List<ExpenseCategory> expenseCategories = new List<ExpenseCategory>();

                //foreach (var category in checkExpenseCategories)
                //{
                //    var test = await _expenseCategoryRepository.GetAll().FirstOrDefaultAsync(x => x.Id == category.IdКатегорииРасхода);
                //    expenseCategories.Add(test);
                    //expenseCategories = await _expenseCategoryRepository.GetAll()
                    //.Where(x => x.Id == category.IdКатегорииРасхода).ToListAsync();//new ExpenseCategory()
                    /*{
                        Id = category.IdСчёта,
                        Название = x.Название
                    })
                    .ToListAsync();*/
                //}

                //expenseCategories.Add(await _expenseCategoryRepository.GetAll().FirstOrDefaultAsync(x => x.Id == category.IdСчёта));
                //var expenseCategories = await _expenseCategoryRepository.GetAll().Where(x => x.Название == model.Name).ToListAsync();
                return new BaseResponse<IEnumerable<Expenses>>()
                {
                    Data = expenses,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<BaseResponse<Expenses>> GetExpenseOnId(int expenseId)
        {
            try
            {
                var expenses = await _expensesRepository.GetAll().FirstOrDefaultAsync(x => x.Id == expenseId);
                return new BaseResponse<Expenses>()
                {
                    Data = expenses,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<IBaseResponse<Expenses>> Create(NewExpenseViewModel model, int checkId)
        {
            var expenseCategory = await _expenseCategoryRepository.GetAll().Where(x => x.Название == model.Category).ToListAsync();
            var checkExpenseCategory = await _checkExpenseCategoryRepository.GetAll().Where(x => x.IdСчёта == checkId).ToListAsync();
            int currentCategoryId = 0;

            try
            {
                foreach (var cat in expenseCategory)
                    foreach (var allCat in checkExpenseCategory)
                        if (cat.Id == allCat.IdКатегорииРасхода)
                        {
                            currentCategoryId = cat.Id;
                            break;
                        }

                var newExpense = new Expenses()
                {
                    Описание = model.Name,
                    Дата = model.Date,
                    IdКатегорииРасхода = currentCategoryId,
                    Сумма = decimal.Parse(model.Sum),
                    IdСчёта = checkId
                    //и т.д.
                };
                await _expensesRepository.Create(newExpense);

                return new BaseResponse<Expenses>()
                {
                    Data = newExpense,
                    Description = "Расход добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ExpenseCategoryService.Create] error: {ex.Message}");
                return new BaseResponse<Expenses>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
            //try
            //{
            //    var checkExpenseCategories = await _checkExpenseCategoryRepository.GetAll().Where(x => x.IdСчёта == id).ToListAsync();
            //    var expenseCategories = await _expenseCategoryRepository.GetAll().Where(x => x.Название == model.Name).ToListAsync();

            //    //foreach (var categoryId in expenseCategories)
            //    //    foreach (var categoryId2 in checkExpenseCategories)
            //    //        if (categoryId2.IdКатегорииРасхода == categoryId.Id)
            //    if (CheckCategoryExists(checkExpenseCategories, expenseCategories))
            //        return new BaseResponse<ExpenseCategory>()
            //        {
            //            Description = "Такая категория уже есть",
            //            StatusCode = StatusCode.ExpenseCategoryAlreadyExists
            //        };
            //    var expenseCategory = new ExpenseCategory()
            //    {
            //        Название = model.Name,
            //        //Изображение = model.Изображение
            //    };

            //    await _expenseCategoryRepository.Create(expenseCategory);

            //    return new BaseResponse<ExpenseCategory>()
            //    {
            //        Data = expenseCategory,
            //        Description = "Категория добавлена",
            //        StatusCode = StatusCode.OK
            //    };
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, $"[ExpenseCategoryService.Create] error: {ex.Message}");
            //    return new BaseResponse<ExpenseCategory>()
            //    {
            //        StatusCode = StatusCode.InternalServerError,
            //        Description = $"Внутренняя ошибка: {ex.Message}"
            //    };
            //}
        }

        public async Task<IBaseResponse<bool>> DeleteExpense(int expenseId)
        {
            try
            {
                var expense = await _expensesRepository.GetAll().FirstOrDefaultAsync(x => x.Id == expenseId);

                if (expense == null)
                {
                    return new BaseResponse<bool>
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }
                await _expensesRepository.Delete(expense);
                _logger.LogInformation($"[UserService.DeleteUser] пользователь удален");

                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.OK,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserSerivce.DeleteUser] error: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
        public async Task<IBaseResponse<Expenses>> ChangeExpense(ChangeExpenseViewModel model, int expenseId, int checkId)
        {
            try
            {

                var expense = await _expensesRepository.GetAll().FirstOrDefaultAsync(x => x.Id == expenseId);

                //var checkExpenseCategories = await _checkExpenseCategoryRepository.GetAll().Where(x => x.IdСчёта == checkId).ToListAsync();
                //var expenseCategories = await _expenseCategoryRepository.GetAll().Where(x => x.Название == model.Name).ToListAsync();

                //if (CheckCategoryExists(checkExpenseCategories, expenseCategories))
                //    return new BaseResponse<ExpenseCategory>()
                //    {
                //        Description = "Такая категория уже есть",
                //        StatusCode = StatusCode.ExpenseCategoryAlreadyExists
                //    };
                var expenseCategory = await _expenseCategoryRepository.GetAll().Where(x => x.Название == model.Category).ToListAsync();
                var checkExpenseCategory = await _checkExpenseCategoryRepository.GetAll().Where(x => x.IdСчёта == checkId).ToListAsync();
                int currentCategoryId = 0;

                foreach (var cat in expenseCategory)
                    foreach (var allCat in checkExpenseCategory)
                        if (cat.Id == allCat.IdКатегорииРасхода)
                        {
                            currentCategoryId = cat.Id;
                            break;
                        }

                if (expense != null)
                {
                    expense.Описание = model.Name;
                    expense.Дата = model.Date;
                    expense.Сумма = decimal.Parse(model.Sum);
                    expense.IdКатегорииРасхода = currentCategoryId;
                    expense.IdСчёта = checkId;
                }


                //var currentExpenseCategory = await _expenseCategoryRepository.GetAll().Where(x => x.Название == oldName).ToListAsync();

                await _expensesRepository.Update(expense);

                //foreach (var expenseCategory in currentExpenseCategory)
                //{
                //    var currentCategoryId = await _checkExpenseCategoryRepository.GetAll().FirstOrDefaultAsync(x => x.IdСчёта == checkId && x.IdКатегорииРасхода == expenseCategory.Id);
                //    if (currentCategoryId != null)
                //    {
                //        expenseCategory.Название = model.Name;
                //        await _expenseCategoryRepository.Update(expenseCategory);
                //        break;
                //    }
                //}

                //var expenseCategoryId = await _checkExpenseCategoryRepository.GetAll().ToListAsync();
                //var categoryName = await _expenseCategoryRepository.GetAll().Where(x => x.Название == name).ToListAsync();

                //foreach (var category in categoryName)
                //{
                //    var test = expenseCategoryId.Where(x => x.IdКатегорииРасхода == category.Id && x.IdСчёта == checkId);
                //    //var expenseCategoryId = await _checkExpenseCategoryRepository.GetAll().FirstOrDefaultAsync(x => x.IdКатегорииРасхода == category.Id && x.IdСчёта == checkId);
                //    if (test != null)
                //    {
                //        await _checkExpenseCategoryRepository.Delete(category.CheckExpenseCategories.FirstOrDefault());
                //        await _expenseCategoryRepository.Delete(category);
                //        break;
                //    }
                //}

                //if (expenseCategory == null)
                //{
                //    return new BaseResponse<bool>
                //    {
                //        StatusCode = StatusCode.UserNotFound,
                //        Data = false
                //    };
                //}
                //await _expenseCategoryRepository.Delete(expenseCategory);
                //_logger.LogInformation($"[UserService.DeleteUser] пользователь удален");

                return new BaseResponse<Expenses>
                {
                    StatusCode = StatusCode.OK,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserSerivce.DeleteUser] error: {ex.Message}");
                return new BaseResponse<Expenses>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
            return null;
        }
    }
}
