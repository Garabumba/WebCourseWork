using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCourseWorkActual.Domain.Entity;
using WebCourseWorkActual.Domain.Response;
using WebCourseWorkActual.Domain.ViewModels.Check;

namespace WebCourseWorkActual.Service.Interfaces
{
    public interface ICheckService
    {
        //Task<IBaseResponse<IEnumerable<User>>> GetUsers();
        Task<IBaseResponse<Check>> Create(CheckViewModel model);

        //BaseResponse<Dictionary<int, string>> GetRoles();

        Task<BaseResponse<IEnumerable<CheckViewModel>>> GetChecks();

        //Task<IBaseResponse<bool>> DeleteUser(int id);
    }
}
