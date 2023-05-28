using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCourseWorkActual.Domain.Entity;
using WebCourseWorkActual.Domain.Response;
using WebCourseWorkActual.Domain.ViewModels.User;

namespace WebCourseWorkActual.Service.Interfaces
{
	public interface IUserService
	{
        //Task<IBaseResponse<IEnumerable<User>>> GetUsers();
        Task<IBaseResponse<User>> Create(UserViewModel model);

        //BaseResponse<Dictionary<int, string>> GetRoles();

        Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers();

        //Task<IBaseResponse<bool>> DeleteUser(int id);
    }
}
