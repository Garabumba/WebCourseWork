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
using WebCourseWorkActual.Domain.Helpers;
using WebCourseWorkActual.Domain.Response;
using WebCourseWorkActual.Domain.ViewModels.User;
using WebCourseWorkActual.Service.Interfaces;

namespace WebCourseWorkActual.Service.Implementations
{
    /*public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<IBaseResponse<User>> GetUser(int id)
		{
			var baseResponse = new BaseResponse<User>();
			try
			{
				var user = await _userRepository.Get(id);
				if (user == null)
				{
					baseResponse.Description = "Пользователь не найден";
					baseResponse.StatusCode = StatusCode.UserNotFound;
					return baseResponse;
				}
				baseResponse.Data = user;
				return baseResponse;
			}
			catch (Exception ex)
			{
				return new BaseResponse<User>()
				{
					Description = $"[GetUser] : { ex.Message }"
				};
			}
		}

		public async Task<IBaseResponse<User>> GetUserByEmail(string email)
		{
			var baseResponse = new BaseResponse<User>();
			try
			{
				var user = await _userRepository.GetByEmail(email);
				if (user == null)
				{
					baseResponse.Description = "Пользователь не найден";
					baseResponse.StatusCode = StatusCode.UserNotFound;
					return baseResponse;
				}
				baseResponse.Data = user;
				return baseResponse;
			}
			catch (Exception ex)
			{
				return new BaseResponse<User>()
				{
					Description = $"[GetUserByEmail] : { ex.Message }"
				};
			}
		}

		public async Task<IBaseResponse<IEnumerable<User>>> GetUsers()
		{
			var baseResponse = new BaseResponse<IEnumerable<User>>();
			try
			{
				var users = await _userRepository.Select();
				if (users.Count == 0)
				{
					baseResponse.Description = "Найдено 0 элементов";
					baseResponse.StatusCode = StatusCode.OK;
					return baseResponse;
				}
				
				baseResponse.Data = users;
				baseResponse.StatusCode = StatusCode.OK;

				return baseResponse;
			}
			catch (Exception ex)
			{
				return new BaseResponse<IEnumerable<User>>()
				{
					Description = $"[GetUsers] : { ex.Message }"
				};
			}
		}

		public async Task<IBaseResponse<UserViewModel>> CreateUser(UserViewModel userViewModel)
		{
			var baseResponse = new BaseResponse<UserViewModel>();
			try
			{
				var user = new User()
				{
					Имя = userViewModel.Имя,
					Фамилия = userViewModel.Фамилия,
					Отчество = userViewModel.Отчество,
					Email = userViewModel.Email,
					Пароль = userViewModel.Пароль
				};

				await _userRepository.Create(user);
			}
			catch (Exception ex)
			{
				return new BaseResponse<UserViewModel>()
				{
					Description = $"[CreateUser] : { ex.Message }",
					StatusCode = StatusCode.InternalServerError
				};
			}
			return baseResponse;
		}
	}*/
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        //private readonly IBaseRepository<Profile> _proFileRepository;
        private readonly IBaseRepository<User> _userRepository;

        public UserService(ILogger<UserService> logger, IBaseRepository<User> userRepository)
            //IBaseRepository<Profile> proFileRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            //_proFileRepository = proFileRepository;
        }

        public async Task<IBaseResponse<User>> Create(UserViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email);
                if (user != null)
                {
                    return new BaseResponse<User>()
                    {
                        Description = "Пользователь с таким email уже есть",
                        StatusCode = StatusCode.UserAlreadyExists
                    };
                }
                user = new User()
                {
                    Имя = model.Имя,
                    Фамилия = model.Фамилия,
                    Отчество = model.Отчество,
                    Email = model.Email,
                    //Role = Enum.Parse<Role>(model.Role),
                    Пароль = HashPasswordHelper.HashPassowrd(model.Пароль),
                };

                await _userRepository.Create(user);

                /*var profile = new Profile()
                {
                    Address = string.Empty,
                    Age = 0,
                    UserId = user.Id,
                };

                await _proFileRepository.Create(profile);
                */
                return new BaseResponse<User>()
                {
                    Data = user,
                    Description = "Пользователь добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserService.Create] error: {ex.Message}");
                return new BaseResponse<User>()
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

        public async Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetAll()
                    .Select(x => new UserViewModel()
                    {
                        Id = x.Id,
                        Имя = x.Имя,
                        Фамилия = x.Фамилия,
                        Отчество = x.Отчество,
                        Email = x.Email,
                        //Role = x.Role.GetDisplayName()
                    })
                    .ToListAsync();

                _logger.LogInformation($"[UserService.GetUsers] получено элементов {users.Count}");
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Data = users,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserSerivce.GetUsers] error: {ex.Message}");
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<User> GetUser(int id)
        {
            try
            {
                return await _userRepository.GetAll().Where(x => x.Id == id).FirstOrDefaultAsync();

                //_logger.LogInformation($"[UserService.GetUser] получено элементов {user.Count}");
                //return user;
                //return new BaseResponse<User>()
                //{
                //    Data = user,
                //    StatusCode = StatusCode.OK
                //};
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            /*catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserSerivce.GetUsers] error: {ex.Message}");
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }*/
        }

        /*public async Task<IBaseResponse<bool>> DeleteUser(long id)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return new BaseResponse<bool>
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }
                await _userRepository.Delete(user);
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
        }*/
    }
}
