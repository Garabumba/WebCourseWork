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
using WebCourseWorkActual.Domain.ViewModels.CheckIncomeCategory;
using WebCourseWorkActual.Domain.ViewModels.Incomes;
using WebCourseWorkActual.Domain.ViewModels.ExpeseCategory;
using WebCourseWorkActual.Domain.ViewModels.Exprnses;
using WebCourseWorkActual.Service.Interfaces;

namespace WebCourseWorkActual.Service.Implementations
{
    //IncomeCategoryService
    public class IncomesService : IIncomesService
    {
        private readonly ILogger<IncomeCategoryService> _logger;
        private readonly IBaseRepository<IncomeCategory> _expenseCategoryRepository;
        private readonly IBaseRepository<CheckIncomeCategory> _checkIncomeCategoryRepository;
        private readonly IBaseRepository<Incomes> _expensesRepository;

        public IncomesService(ILogger<IncomeCategoryService> logger, IBaseRepository<Incomes> expenses, IBaseRepository<IncomeCategory> expenseCategory, IBaseRepository<CheckIncomeCategory> checkIncomeCategory)
        {
            _logger = logger;
            _expensesRepository = expenses;
            _expenseCategoryRepository = expenseCategory;
            _checkIncomeCategoryRepository = checkIncomeCategory;
        }
        public async Task<BaseResponse<IEnumerable<Incomes>>> GetIncomes(int id)
        {
            try
            {
                var expenses = await _expensesRepository.GetAll().Where(x => x.IdСчёта == id).ToListAsync();

                //var checkIncomeCategories = await _checkIncomeCategoryRepository.GetAll().Where(x => x.IdСчёта == id).ToListAsync();
                //List<IncomeCategory> expenseCategories = new List<IncomeCategory>();

                //foreach (var category in checkIncomeCategories)
                //{
                //    var test = await _expenseCategoryRepository.GetAll().FirstOrDefaultAsync(x => x.Id == category.IdКатегорииДохода);
                //    expenseCategories.Add(test);
                //expenseCategories = await _expenseCategoryRepository.GetAll()
                //.Where(x => x.Id == category.IdКатегорииДохода).ToListAsync();//new IncomeCategory()
                /*{
                    Id = category.IdСчёта,
                    Название = x.Название
                })
                .ToListAsync();*/
                //}

                //expenseCategories.Add(await _expenseCategoryRepository.GetAll().FirstOrDefaultAsync(x => x.Id == category.IdСчёта));
                //var expenseCategories = await _expenseCategoryRepository.GetAll().Where(x => x.Название == model.Name).ToListAsync();
                return new BaseResponse<IEnumerable<Incomes>>()
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

        public async Task<BaseResponse<Incomes>> GetIncomeOnId(int expenseId)
        {
            try
            {
                var expenses = await _expensesRepository.GetAll().FirstOrDefaultAsync(x => x.Id == expenseId);
                return new BaseResponse<Incomes>()
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

        public async Task<IBaseResponse<Incomes>> Create(NewIncomeViewModel model, int checkId)
        {
            var expenseCategory = await _expenseCategoryRepository.GetAll().Where(x => x.Название == model.Category).ToListAsync();
            var checkIncomeCategory = await _checkIncomeCategoryRepository.GetAll().Where(x => x.IdСчёта == checkId).ToListAsync();
            int currentCategoryId = 0;

            try
            {
                foreach (var cat in expenseCategory)
                    foreach (var allCat in checkIncomeCategory)
                        if (cat.Id == allCat.IdКатегорииДохода)
                        {
                            currentCategoryId = cat.Id;
                            break;
                        }

                var newIncome = new Incomes()
                {
                    Описание = model.Name,
                    Дата = model.Date,
                    IdКатегорииДохода = currentCategoryId,
                    Сумма = decimal.Parse(model.Sum),
                    IdСчёта = checkId
                    //и т.д.
                };
                await _expensesRepository.Create(newIncome);

                return new BaseResponse<Incomes>()
                {
                    Data = newIncome,
                    Description = "Доход добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[IncomeCategoryService.Create] error: {ex.Message}");
                return new BaseResponse<Incomes>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
            //try
            //{
            //    var checkIncomeCategories = await _checkIncomeCategoryRepository.GetAll().Where(x => x.IdСчёта == id).ToListAsync();
            //    var expenseCategories = await _expenseCategoryRepository.GetAll().Where(x => x.Название == model.Name).ToListAsync();

            //    //foreach (var categoryId in expenseCategories)
            //    //    foreach (var categoryId2 in checkIncomeCategories)
            //    //        if (categoryId2.IdКатегорииДохода == categoryId.Id)
            //    if (CheckCategoryExists(checkIncomeCategories, expenseCategories))
            //        return new BaseResponse<IncomeCategory>()
            //        {
            //            Description = "Такая категория уже есть",
            //            StatusCode = StatusCode.IncomeCategoryAlreadyExists
            //        };
            //    var expenseCategory = new IncomeCategory()
            //    {
            //        Название = model.Name,
            //        //Изображение = model.Изображение
            //    };

            //    await _expenseCategoryRepository.Create(expenseCategory);

            //    return new BaseResponse<IncomeCategory>()
            //    {
            //        Data = expenseCategory,
            //        Description = "Категория добавлена",
            //        StatusCode = StatusCode.OK
            //    };
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, $"[IncomeCategoryService.Create] error: {ex.Message}");
            //    return new BaseResponse<IncomeCategory>()
            //    {
            //        StatusCode = StatusCode.InternalServerError,
            //        Description = $"Внутренняя ошибка: {ex.Message}"
            //    };
            //}
        }

        public async Task<IBaseResponse<bool>> DeleteIncome(int expenseId)
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
        public async Task<IBaseResponse<Incomes>> ChangeIncome(ChangeIncomeViewModel model, int expenseId, int checkId)
        {
            try
            {

                var expense = await _expensesRepository.GetAll().FirstOrDefaultAsync(x => x.Id == expenseId);

                //var checkIncomeCategories = await _checkIncomeCategoryRepository.GetAll().Where(x => x.IdСчёта == checkId).ToListAsync();
                //var expenseCategories = await _expenseCategoryRepository.GetAll().Where(x => x.Название == model.Name).ToListAsync();

                //if (CheckCategoryExists(checkIncomeCategories, expenseCategories))
                //    return new BaseResponse<IncomeCategory>()
                //    {
                //        Description = "Такая категория уже есть",
                //        StatusCode = StatusCode.IncomeCategoryAlreadyExists
                //    };
                var expenseCategory = await _expenseCategoryRepository.GetAll().Where(x => x.Название == model.Category).ToListAsync();
                var checkIncomeCategory = await _checkIncomeCategoryRepository.GetAll().Where(x => x.IdСчёта == checkId).ToListAsync();
                int currentCategoryId = 0;

                foreach (var cat in expenseCategory)
                    foreach (var allCat in checkIncomeCategory)
                        if (cat.Id == allCat.IdКатегорииДохода)
                        {
                            currentCategoryId = cat.Id;
                            break;
                        }

                if (expense != null)
                {
                    expense.Описание = model.Name;
                    expense.Дата = model.Date;
                    expense.Сумма = decimal.Parse(model.Sum);
                    expense.IdКатегорииДохода = currentCategoryId;
                    expense.IdСчёта = checkId;
                }


                //var currentIncomeCategory = await _expenseCategoryRepository.GetAll().Where(x => x.Название == oldName).ToListAsync();

                await _expensesRepository.Update(expense);

                //foreach (var expenseCategory in currentIncomeCategory)
                //{
                //    var currentCategoryId = await _checkIncomeCategoryRepository.GetAll().FirstOrDefaultAsync(x => x.IdСчёта == checkId && x.IdКатегорииДохода == expenseCategory.Id);
                //    if (currentCategoryId != null)
                //    {
                //        expenseCategory.Название = model.Name;
                //        await _expenseCategoryRepository.Update(expenseCategory);
                //        break;
                //    }
                //}

                //var expenseCategoryId = await _checkIncomeCategoryRepository.GetAll().ToListAsync();
                //var categoryName = await _expenseCategoryRepository.GetAll().Where(x => x.Название == name).ToListAsync();

                //foreach (var category in categoryName)
                //{
                //    var test = expenseCategoryId.Where(x => x.IdКатегорииДохода == category.Id && x.IdСчёта == checkId);
                //    //var expenseCategoryId = await _checkIncomeCategoryRepository.GetAll().FirstOrDefaultAsync(x => x.IdКатегорииДохода == category.Id && x.IdСчёта == checkId);
                //    if (test != null)
                //    {
                //        await _checkIncomeCategoryRepository.Delete(category.CheckIncomeCategories.FirstOrDefault());
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

                return new BaseResponse<Incomes>
                {
                    StatusCode = StatusCode.OK,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserSerivce.DeleteUser] error: {ex.Message}");
                return new BaseResponse<Incomes>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
            return null;
        }
    }
}
