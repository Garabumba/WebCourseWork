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
using WebCourseWorkActual.Service.Interfaces;

namespace WebCourseWorkActual.Service.Implementations
{
    public class CheckExpenseCategoryService : ICheckExpenseCategoryService
    {
        private readonly ILogger<CheckExpenseCategoryService> _logger;
        private readonly IBaseRepository<CheckExpenseCategory> _checkExpenseCategoryRepository;

        public CheckExpenseCategoryService(ILogger<CheckExpenseCategoryService> logger, IBaseRepository<CheckExpenseCategory> checkExpenseCategoryRepository)
        {
            _logger = logger;
            _checkExpenseCategoryRepository = checkExpenseCategoryRepository;
        }

        public async Task<IBaseResponse<CheckExpenseCategory>> Create(CheckExpenseCategoryViewModel model)
        {
            try
            {
                var checkExpenseCategory = await _checkExpenseCategoryRepository.GetAll().FirstOrDefaultAsync(x => x.IdСчёта == model.IdСчёта && x.IdКатегорииРасхода == model.IdКатегорииРасхода);
                if (checkExpenseCategory != null)
                {
                    return new BaseResponse<CheckExpenseCategory>()
                    {
                        Description = "Пользователь с таким email уже есть",
                        StatusCode = StatusCode.ExpenseCategoryAlreadyExists
                    };
                }
                checkExpenseCategory = new CheckExpenseCategory()
                {
                    IdСчёта = model.IdСчёта,
                    IdКатегорииРасхода = model.IdКатегорииРасхода
                };

                await _checkExpenseCategoryRepository.Create(checkExpenseCategory);

                return new BaseResponse<CheckExpenseCategory>()
                {
                    Data = checkExpenseCategory,
                    Description = "Пользователь добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[CheckExpenseCategoryService.Create] error: {ex.Message}");
                return new BaseResponse<CheckExpenseCategory>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<CheckExpenseCategoryViewModel>>> GetCheckExpenseCategories(int id)
        {
            try
            {
                var checkExpenseCategories = await _checkExpenseCategoryRepository.GetAll().Where(x => x.IdСчёта == id)
                    .Select(x => new CheckExpenseCategoryViewModel()
                    {
                        IdСчёта = id,
                        IdКатегорииРасхода = x.IdКатегорииРасхода
                    })
                    .ToListAsync();

                _logger.LogInformation($"[CheckExpenseCategoryService.GetCheckExpenseCategorys] получено элементов {checkExpenseCategories.Count}");
                return new BaseResponse<IEnumerable<CheckExpenseCategoryViewModel>>()
                {
                    Data = checkExpenseCategories,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[CheckExpenseCategorySerivce.GetCheckExpenseCategorys] error: {ex.Message}");
                return new BaseResponse<IEnumerable<CheckExpenseCategoryViewModel>>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
        public async Task<IBaseResponse<bool>> DeleteCheckExpenseCategory(int checkId, int expenseCategoryId)
        {
            try
            {
                var checkExpenseCategory = await _checkExpenseCategoryRepository.GetAll().FirstOrDefaultAsync(x => x.IdСчёта == checkId && x.IdКатегорииРасхода == expenseCategoryId);

                if (checkExpenseCategory == null)
                {
                    return new BaseResponse<bool>
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }
                await _checkExpenseCategoryRepository.Delete(checkExpenseCategory);
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
    }
}
