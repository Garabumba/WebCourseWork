using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCourseWorkActual.Domain.Entity;
using WebCourseWorkActual.Domain.Response;
using WebCourseWorkActual.Domain.ViewModels.CheckExpenseCategory;

namespace WebCourseWorkActual.Service.Interfaces
{
    public interface ICheckExpenseCategoryService
    {
        //Task<IBaseResponse<IEnumerable<CheckExpenseCategory>>> GetCheckExpenseCategorys();
        Task<IBaseResponse<CheckExpenseCategory>> Create(CheckExpenseCategoryViewModel model);

        //BaseResponse<Dictionary<int, string>> GetRoles();

        Task<BaseResponse<IEnumerable<CheckExpenseCategoryViewModel>>> GetCheckExpenseCategories(int id);
        //Task<CheckExpenseCategory> GetCheckExpenseCategories(int id);

        Task<IBaseResponse<bool>> DeleteCheckExpenseCategory(int checkId, int expenseCategoryId);
    }
}
