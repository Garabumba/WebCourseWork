using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCourseWorkActual.Domain.Entity;
using WebCourseWorkActual.Domain.Response;
using WebCourseWorkActual.Domain.ViewModels.CheckIncomeCategory;

namespace WebCourseWorkActual.Service.Interfaces
{
    public interface ICheckIncomeCategoryService
    {
        //Task<IBaseResponse<IEnumerable<CheckIncomeCategory>>> GetCheckIncomeCategorys();
        Task<IBaseResponse<CheckIncomeCategory>> Create(CheckIncomeCategoryViewModel model);

        //BaseResponse<Dictionary<int, string>> GetRoles();

        Task<BaseResponse<IEnumerable<CheckIncomeCategoryViewModel>>> GetCheckIncomeCategories(int id);
        //Task<CheckIncomeCategory> GetCheckIncomeCategories(int id);

        Task<IBaseResponse<bool>> DeleteCheckIncomeCategory(int checkId, int expenseCategoryId);
    }
}
