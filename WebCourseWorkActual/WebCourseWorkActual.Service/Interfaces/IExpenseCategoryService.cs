using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCourseWorkActual.Domain.Entity;
using WebCourseWorkActual.Domain.Response;
using WebCourseWorkActual.Domain.ViewModels.CheckExpenseCategory;
using WebCourseWorkActual.Domain.ViewModels.ExpeseCategory;
using WebCourseWorkActual.Domain.ViewModels.Exprnses;

namespace WebCourseWorkActual.Service.Interfaces
{
    public interface IExpenseCategoryService
    {
        //Task<IBaseResponse<IEnumerable<User>>> GetUsers();
        Task<IBaseResponse<ExpenseCategory>> Create(NewExpenseCategoryViewModel model, int id);

        //BaseResponse<Dictionary<int, string>> GetRoles();

        //Task<BaseResponse<IEnumerable<ExpenseCategoryViewModel>>> GetUsers();
        Task<BaseResponse<IEnumerable<ExpenseCategory>>> GetExpenseCategories(int id);
        Task<IBaseResponse<bool>> DeleteExpenseCategory(string name, int checkId);
        Task<IBaseResponse<ExpenseCategory>> ChangeExpenseCategory(ChangeExpenseCategoryViewModel model, int checkId, string oldName);
    }
}
