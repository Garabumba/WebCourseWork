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
using WebCourseWorkActual.Domain.ViewModels.ExpeseCategory;
using WebCourseWorkActual.Domain.ViewModels.Exprnses;
using WebCourseWorkActual.Service.Interfaces;

namespace WebCourseWorkActual.Service.Implementations
{
    //ExpenseCategoryService
    public class ExpenseCategoryService : IExpenseCategoryService
    {
        private readonly ILogger<ExpenseCategoryService> _logger;
        private readonly IBaseRepository<ExpenseCategory> _expenseCategoryRepository;
        private readonly IBaseRepository<CheckExpenseCategory> _checkExpenseCategoryRepository;

        public ExpenseCategoryService(ILogger<ExpenseCategoryService> logger, IBaseRepository<ExpenseCategory> expenseCategoryRepository, IBaseRepository<CheckExpenseCategory> checkExpenseCategoryRepository)
        {
            _logger = logger;
            _expenseCategoryRepository = expenseCategoryRepository;
            _checkExpenseCategoryRepository = checkExpenseCategoryRepository;
        }
        public async Task<BaseResponse<IEnumerable<ExpenseCategory>>> GetExpenseCategories(int id)
        {
            try
            {
                var checkExpenseCategories = await _checkExpenseCategoryRepository.GetAll().Where(x => x.IdСчёта == id).ToListAsync();
                List<ExpenseCategory> expenseCategories = new List<ExpenseCategory>();

                foreach (var category in checkExpenseCategories)
                {
                    var test = await _expenseCategoryRepository.GetAll().FirstOrDefaultAsync(x => x.Id == category.IdКатегорииРасхода);
                    expenseCategories.Add(test);
                    //expenseCategories = await _expenseCategoryRepository.GetAll()
                    //.Where(x => x.Id == category.IdКатегорииРасхода).ToListAsync();//new ExpenseCategory()
                    /*{
                        Id = category.IdСчёта,
                        Название = x.Название
                    })
                    .ToListAsync();*/
                }

                //expenseCategories.Add(await _expenseCategoryRepository.GetAll().FirstOrDefaultAsync(x => x.Id == category.IdСчёта));
                //var expenseCategories = await _expenseCategoryRepository.GetAll().Where(x => x.Название == model.Name).ToListAsync();
                return new BaseResponse<IEnumerable<ExpenseCategory>>()
                {
                    Data = expenseCategories,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private bool CheckCategoryExists(List<CheckExpenseCategory> checkExpenseCategories, List<ExpenseCategory> expenseCategories)
        {
            foreach (var categoryId in expenseCategories)
                foreach (var categoryId2 in checkExpenseCategories)
                    if (categoryId2.IdКатегорииРасхода == categoryId.Id)
                        return true;
            return false;
        }

        public async Task<IBaseResponse<ExpenseCategory>> Create(NewExpenseCategoryViewModel model, int id)
        {
            try
            {
                var checkExpenseCategories = await _checkExpenseCategoryRepository.GetAll().Where(x => x.IdСчёта == id).ToListAsync();
                var expenseCategories = await _expenseCategoryRepository.GetAll().Where(x => x.Название == model.Name).ToListAsync();
                
                //foreach (var categoryId in expenseCategories)
                //    foreach (var categoryId2 in checkExpenseCategories)
                //        if (categoryId2.IdКатегорииРасхода == categoryId.Id)
                if (CheckCategoryExists(checkExpenseCategories, expenseCategories))
                    return new BaseResponse<ExpenseCategory>()
                    {
                        Description = "Такая категория уже есть",
                        StatusCode = StatusCode.ExpenseCategoryAlreadyExists
                    };
                var expenseCategory = new ExpenseCategory()
                {
                    Название = model.Name,
                    //Изображение = model.Изображение
                };

                await _expenseCategoryRepository.Create(expenseCategory);

                return new BaseResponse<ExpenseCategory>()
                {
                    Data = expenseCategory,
                    Description = "Категория добавлена",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ExpenseCategoryService.Create] error: {ex.Message}");
                return new BaseResponse<ExpenseCategory>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteExpenseCategory(string name, int checkId)
        {
            try
            {
                var expenseCategoryId = await _checkExpenseCategoryRepository.GetAll().ToListAsync();
                var categoryName = await _expenseCategoryRepository.GetAll().Where(x => x.Название == name).ToListAsync();
                
                foreach (var category in categoryName)
                {
                    var test = expenseCategoryId.Where(x => x.IdКатегорииРасхода == category.Id && x.IdСчёта == checkId);
                    //var expenseCategoryId = await _checkExpenseCategoryRepository.GetAll().FirstOrDefaultAsync(x => x.IdКатегорииРасхода == category.Id && x.IdСчёта == checkId);
                    if (test != null)
                    {
                        await _checkExpenseCategoryRepository.Delete(category.CheckExpenseCategories.FirstOrDefault());
                        await _expenseCategoryRepository.Delete(category);
                        break;
                    }
                }

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

        public async Task<IBaseResponse<ExpenseCategory>> ChangeExpenseCategory(ChangeExpenseCategoryViewModel model, int checkId, string oldName)
        {
            try
            {
                var checkExpenseCategories = await _checkExpenseCategoryRepository.GetAll().Where(x => x.IdСчёта == checkId).ToListAsync();
                var expenseCategories = await _expenseCategoryRepository.GetAll().Where(x => x.Название == model.Name).ToListAsync();

                if (CheckCategoryExists(checkExpenseCategories, expenseCategories))
                    return new BaseResponse<ExpenseCategory>()
                    {
                        Description = "Такая категория уже есть",
                        StatusCode = StatusCode.ExpenseCategoryAlreadyExists
                    };

                var currentExpenseCategory = await _expenseCategoryRepository.GetAll().Where(x => x.Название == oldName).ToListAsync();
                
                foreach (var expenseCategory in currentExpenseCategory)
                {
                    var currentCategoryId = await _checkExpenseCategoryRepository.GetAll().FirstOrDefaultAsync(x => x.IdСчёта == checkId && x.IdКатегорииРасхода == expenseCategory.Id);
                    if (currentCategoryId != null)
                    {
                        expenseCategory.Название = model.Name;
                        await _expenseCategoryRepository.Update(expenseCategory);
                        break;
                    }
                }
                
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

                return new BaseResponse<ExpenseCategory>
                {
                    StatusCode = StatusCode.OK,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserSerivce.DeleteUser] error: {ex.Message}");
                return new BaseResponse<ExpenseCategory>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
