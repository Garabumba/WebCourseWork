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
using WebCourseWorkActual.Domain.ViewModels.ExpeseCategory;
using WebCourseWorkActual.Domain.ViewModels.Exprnses;
using WebCourseWorkActual.Domain.ViewModels.IncomeCategory;
using WebCourseWorkActual.Service.Interfaces;

namespace WebCourseWorkActual.Service.Implementations
{
    //IncomeCategoryService
    public class IncomeCategoryService : IIncomeCategoryService
    {
        private readonly ILogger<IncomeCategoryService> _logger;
        private readonly IBaseRepository<IncomeCategory> _incomeCategoryRepository;
        private readonly IBaseRepository<CheckIncomeCategory> _checkIncomeCategoryRepository;

        public IncomeCategoryService(ILogger<IncomeCategoryService> logger, IBaseRepository<IncomeCategory> incomeCategoryRepository, IBaseRepository<CheckIncomeCategory> checkIncomeCategoryRepository)
        {
            _logger = logger;
            _incomeCategoryRepository = incomeCategoryRepository;
            _checkIncomeCategoryRepository = checkIncomeCategoryRepository;
        }
        public async Task<BaseResponse<IEnumerable<IncomeCategory>>> GetIncomeCategories(int id)
        {
            try
            {
                var checkIncomeCategories = await _checkIncomeCategoryRepository.GetAll().Where(x => x.IdСчёта == id).ToListAsync();
                List<IncomeCategory> incomeCategories = new List<IncomeCategory>();

                foreach (var category in checkIncomeCategories)
                {
                    var test = await _incomeCategoryRepository.GetAll().FirstOrDefaultAsync(x => x.Id == category.IdКатегорииДохода);
                    incomeCategories.Add(test);
                    //incomeCategories = await _incomeCategoryRepository.GetAll()
                    //.Where(x => x.Id == category.IdКатегорииРасхода).ToListAsync();//new IncomeCategory()
                    /*{
                        Id = category.IdСчёта,
                        Название = x.Название
                    })
                    .ToListAsync();*/
                }

                //incomeCategories.Add(await _incomeCategoryRepository.GetAll().FirstOrDefaultAsync(x => x.Id == category.IdСчёта));
                //var incomeCategories = await _incomeCategoryRepository.GetAll().Where(x => x.Название == model.Name).ToListAsync();
                return new BaseResponse<IEnumerable<IncomeCategory>>()
                {
                    Data = incomeCategories,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private bool CheckCategoryExists(List<CheckIncomeCategory> checkIncomeCategories, List<IncomeCategory> incomeCategories)
        {
            foreach (var categoryId in incomeCategories)
                foreach (var categoryId2 in checkIncomeCategories)
                    if (categoryId2.IdКатегорииДохода == categoryId.Id)
                        return true;
            return false;
        }

        public async Task<IBaseResponse<IncomeCategory>> Create(NewIncomeCategoryViewModel model, int id)
        {
            try
            {
                var checkIncomeCategories = await _checkIncomeCategoryRepository.GetAll().Where(x => x.IdСчёта == id).ToListAsync();
                var incomeCategories = await _incomeCategoryRepository.GetAll().Where(x => x.Название == model.Name).ToListAsync();

                //foreach (var categoryId in incomeCategories)
                //    foreach (var categoryId2 in checkIncomeCategories)
                //        if (categoryId2.IdКатегорииРасхода == categoryId.Id)
                if (CheckCategoryExists(checkIncomeCategories, incomeCategories))
                    return new BaseResponse<IncomeCategory>()
                    {
                        Description = "Такая категория уже есть",
                        StatusCode = StatusCode.ExpenseCategoryAlreadyExists
                    };
                var incomeCategory = new IncomeCategory()
                {
                    Название = model.Name,
                    //Изображение = model.Изображение
                };

                await _incomeCategoryRepository.Create(incomeCategory);

                return new BaseResponse<IncomeCategory>()
                {
                    Data = incomeCategory,
                    Description = "Категория добавлена",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[IncomeCategoryService.Create] error: {ex.Message}");
                return new BaseResponse<IncomeCategory>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteIncomeCategory(string name, int checkId)
        {
            try
            {
                var incomeCategoryId = await _checkIncomeCategoryRepository.GetAll().ToListAsync();
                var categoryName = await _incomeCategoryRepository.GetAll().Where(x => x.Название == name).ToListAsync();

                foreach (var category in categoryName)
                {
                    var test = incomeCategoryId.Where(x => x.IdКатегорииДохода == category.Id && x.IdСчёта == checkId);
                    //var incomeCategoryId = await _checkIncomeCategoryRepository.GetAll().FirstOrDefaultAsync(x => x.IdКатегорииРасхода == category.Id && x.IdСчёта == checkId);
                    if (test != null)
                    {
                        await _checkIncomeCategoryRepository.Delete(category.CheckIncomeCategories.FirstOrDefault());
                        await _incomeCategoryRepository.Delete(category);
                        break;
                    }
                }

                //if (incomeCategory == null)
                //{
                //    return new BaseResponse<bool>
                //    {
                //        StatusCode = StatusCode.UserNotFound,
                //        Data = false
                //    };
                //}
                //await _incomeCategoryRepository.Delete(incomeCategory);
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

        public async Task<IBaseResponse<IncomeCategory>> ChangeIncomeCategory(ChangeIncomeCategoryViewModel model, int checkId, string oldName)
        {
            try
            {
                var checkIncomeCategories = await _checkIncomeCategoryRepository.GetAll().Where(x => x.IdСчёта == checkId).ToListAsync();
                var incomeCategories = await _incomeCategoryRepository.GetAll().Where(x => x.Название == model.Name).ToListAsync();

                if (CheckCategoryExists(checkIncomeCategories, incomeCategories))
                    return new BaseResponse<IncomeCategory>()
                    {
                        Description = "Такая категория уже есть",
                        StatusCode = StatusCode.ExpenseCategoryAlreadyExists
                    };

                var currentIncomeCategory = await _incomeCategoryRepository.GetAll().Where(x => x.Название == oldName).ToListAsync();

                foreach (var incomeCategory in currentIncomeCategory)
                {
                    var currentCategoryId = await _checkIncomeCategoryRepository.GetAll().FirstOrDefaultAsync(x => x.IdСчёта == checkId && x.IdКатегорииДохода == incomeCategory.Id);
                    if (currentCategoryId != null)
                    {
                        incomeCategory.Название = model.Name;
                        await _incomeCategoryRepository.Update(incomeCategory);
                        break;
                    }
                }

                //var incomeCategoryId = await _checkIncomeCategoryRepository.GetAll().ToListAsync();
                //var categoryName = await _incomeCategoryRepository.GetAll().Where(x => x.Название == name).ToListAsync();

                //foreach (var category in categoryName)
                //{
                //    var test = incomeCategoryId.Where(x => x.IdКатегорииРасхода == category.Id && x.IdСчёта == checkId);
                //    //var incomeCategoryId = await _checkIncomeCategoryRepository.GetAll().FirstOrDefaultAsync(x => x.IdКатегорииРасхода == category.Id && x.IdСчёта == checkId);
                //    if (test != null)
                //    {
                //        await _checkIncomeCategoryRepository.Delete(category.CheckIncomeCategories.FirstOrDefault());
                //        await _incomeCategoryRepository.Delete(category);
                //        break;
                //    }
                //}

                //if (incomeCategory == null)
                //{
                //    return new BaseResponse<bool>
                //    {
                //        StatusCode = StatusCode.UserNotFound,
                //        Data = false
                //    };
                //}
                //await _incomeCategoryRepository.Delete(incomeCategory);
                //_logger.LogInformation($"[UserService.DeleteUser] пользователь удален");

                return new BaseResponse<IncomeCategory>
                {
                    StatusCode = StatusCode.OK,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserSerivce.DeleteUser] error: {ex.Message}");
                return new BaseResponse<IncomeCategory>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
