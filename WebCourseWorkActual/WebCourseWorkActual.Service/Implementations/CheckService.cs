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
using WebCourseWorkActual.Domain.ViewModels.Check;
using WebCourseWorkActual.Domain.ViewModels.User;
using WebCourseWorkActual.Service.Interfaces;

namespace WebCourseWorkActual.Service.Implementations
{
    public class CheckService : ICheckService
    {
        private readonly ILogger<CheckService> _logger;
        //private readonly IBaseRepository<Profile> _proFileRepository;
        private readonly IBaseRepository<Check> _checkRepository;

        public CheckService(ILogger<CheckService> logger, IBaseRepository<Check> checkRepository)
        //IBaseRepository<Profile> proFileRepository)
        {
            _logger = logger;
            _checkRepository = checkRepository;
            //_proFileRepository = proFileRepository;
        }

        public async Task<IBaseResponse<Check>> Create(CheckViewModel model)
        {
            try
            {
                var check = await _checkRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
                /*if (check != null)
                {
                    return new BaseResponse<Check>()
                    {
                        Description = "Пользователь с таким email уже есть",
                        StatusCode = StatusCode.CheckAlreadyExists
                    };
                }*/
                check = new Check()
                {
                    Id = model.Id,
                    Баланс = 0,
                    IdПользователя = model.Id
                };

                await _checkRepository.Create(check);

                /*var profile = new Profile()
                {
                    Address = string.Empty,
                    Age = 0,
                    CheckId = check.Id,
                };

                await _proFileRepository.Create(profile);
                */
                return new BaseResponse<Check>()
                {
                    Data = check,
                    Description = "Счёт добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[CheckService.Create] error: {ex.Message}");
                return new BaseResponse<Check>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        /*public BaseResponse<Dictionary<int, string>> GetRoles()
        {
            try
            {
                var roles = ((Role[])Enum.GetValues(typeof(Role)))
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());

                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = roles,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }*/

        public async Task<BaseResponse<IEnumerable<CheckViewModel>>> GetChecks()
        {
            try
            {
                var checks = await _checkRepository.GetAll()
                    .Select(x => new CheckViewModel()
                    {
                        Id = x.Id,
                        Баланс = x.Баланс
                        //Role = x.Role.GetDisplayName()
                    })
                    .ToListAsync();

                _logger.LogInformation($"[CheckService.GetChecks] получено элементов {checks.Count}");
                return new BaseResponse<IEnumerable<CheckViewModel>>()
                {
                    Data = checks,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[CheckSerivce.GetChecks] error: {ex.Message}");
                return new BaseResponse<IEnumerable<CheckViewModel>>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        /*public async Task<IBaseResponse<bool>> DeleteCheck(long id)
        {
            try
            {
                var check = await _checkRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (check == null)
                {
                    return new BaseResponse<bool>
                    {
                        StatusCode = StatusCode.CheckNotFound,
                        Data = false
                    };
                }
                await _checkRepository.Delete(check);
                _logger.LogInformation($"[CheckService.DeleteCheck] пользователь удален");

                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.OK,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[CheckSerivce.DeleteCheck] error: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }*/
    }
}
