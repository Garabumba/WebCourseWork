using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebCourseWorkActual.DAL.Interfaces;
using WebCourseWorkActual.Domain.Entity;
using WebCourseWorkActual.Domain.Enum;
using WebCourseWorkActual.Domain.Helpers;
using WebCourseWorkActual.Domain.Response;
using WebCourseWorkActual.Domain.ViewModels.Account;
using WebCourseWorkActual.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WebCourseWorkActual.Service.Implementations
{
    public class AccountService : IAccountService
    {
        //private readonly IBaseRepository<Profile> _proFileRepository;
        private readonly IBaseRepository<User> _userRepository;
        //private readonly IBaseRepository<Basket> _basketRepository;
        private readonly ILogger<AccountService> _logger;
        
        public AccountService(IBaseRepository<User> userRepository,
            ILogger<AccountService> logger)//, IBaseRepository<Profile> proFileRepository,
            //IBaseRepository<Basket> basketRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
            //_proFileRepository = proFileRepository;
            //_basketRepository = basketRepository;
        }

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email);
                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь с таким email уже есть",
                    };
                }

                user = new User()
                {
                    Имя = model.Name,
                    Фамилия = model.SurName,
                    Отчество = model.Patronymic,
                    Email = model.Email,
                    //Role = Role.User,
                    Пароль = HashPasswordHelper.HashPassowrd(model.Password)
                };

                await _userRepository.Create(user);

                /*var profile = new Profile()
                {
                    UserId = user.Id,
                };

                var basket = new Basket()
                {
                    UserId = user.Id
                }; 
                
                await _proFileRepository.Create(profile);
                await _basketRepository.Create(basket);*/
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Объект добавился",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Register]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email);
                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь не найден"
                    };
                }

                if (user.Пароль != HashPasswordHelper.HashPassowrd(model.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Неверный пароль или логин"
                    };
                }
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Login]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        /*public async Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.UserName);
                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Description = "Пользователь не найден"
                    };
                }

                user.Password = HashPasswordHelper.HashPassowrd(model.NewPassword);
                await _userRepository.Update(user);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK,
                    Description = "Пароль обновлен"
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ChangePassword]: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }*/

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Email)
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}