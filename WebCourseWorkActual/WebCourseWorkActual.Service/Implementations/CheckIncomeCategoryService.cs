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
using WebCourseWorkActual.Service.Interfaces;

namespace WebCourseWorkActual.Service.Implementations
{
    public class CheckIncomeCategoryService : ICheckIncomeCategoryService
    {
        private readonly ILogger<CheckIncomeCategoryService> _logger;
        private readonly IBaseRepository<CheckIncomeCategory> _checkIncomeCategoryRepository;

        public CheckIncomeCategoryService(ILogger<CheckIncomeCategoryService> logger, IBaseRepository<CheckIncomeCategory> checkIncomeCategoryRepository)
        {
            _logger = logger;
            _checkIncomeCategoryRepository = checkIncomeCategoryRepository;
        }

        public async Task<IBaseResponse<CheckIncomeCategory>> Create(CheckIncomeCategoryViewModel model)
        {
            try
            {
                var checkIncomeCategory = await _checkIncomeCategoryRepository.GetAll().FirstOrDefaultAsync(x => x.IdСчёта == model.IdСчёта && x.IdКатегорииДохода == model.IdКатегорииДохода);
                if (checkIncomeCategory != null)
                {
                    return new BaseResponse<CheckIncomeCategory>()
                    {
                        Description = "Пользователь с таким email уже есть",
                        StatusCode = StatusCode.UserAlreadyExists
                    };
                }
                checkIncomeCategory = new CheckIncomeCategory()
                {
                    IdСчёта = model.IdСчёта,
                    IdКатегорииДохода = model.IdКатегорииДохода
                };

                await _checkIncomeCategoryRepository.Create(checkIncomeCategory);

                return new BaseResponse<CheckIncomeCategory>()
                {
                    Data = checkIncomeCategory,
                    Description = "Пользователь добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[CheckIncomeCategoryService.Create] error: {ex.Message}");
                return new BaseResponse<CheckIncomeCategory>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<CheckIncomeCategoryViewModel>>> GetCheckIncomeCategories(int id)
        {
            try
            {
                var checkIncomeCategories = await _checkIncomeCategoryRepository.GetAll().Where(x => x.IdСчёта == id)
                    .Select(x => new CheckIncomeCategoryViewModel()
                    {
                        IdСчёта = id,
                        IdКатегорииДохода = x.IdКатегорииДохода
                    })
                    .ToListAsync();

                _logger.LogInformation($"[CheckIncomeCategoryService.GetCheckIncomeCategorys] получено элементов {checkIncomeCategories.Count}");
                return new BaseResponse<IEnumerable<CheckIncomeCategoryViewModel>>()
                {
                    Data = checkIncomeCategories,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[CheckIncomeCategorySerivce.GetCheckIncomeCategorys] error: {ex.Message}");
                return new BaseResponse<IEnumerable<CheckIncomeCategoryViewModel>>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
        public async Task<IBaseResponse<bool>> DeleteCheckIncomeCategory(int checkId, int expenseCategoryId)
        {
            try
            {
                var checkIncomeCategory = await _checkIncomeCategoryRepository.GetAll().FirstOrDefaultAsync(x => x.IdСчёта == checkId && x.IdКатегорииДохода == expenseCategoryId);

                if (checkIncomeCategory == null)
                {
                    return new BaseResponse<bool>
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }
                await _checkIncomeCategoryRepository.Delete(checkIncomeCategory);
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
