using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCourseWorkActual.Domain.Entity;
using WebCourseWorkActual.Domain.Response;
using WebCourseWorkActual.Domain.ViewModels.CheckExpenseCategory;
using WebCourseWorkActual.Domain.ViewModels.Expenses;
using WebCourseWorkActual.Domain.ViewModels.ExpeseCategory;
using WebCourseWorkActual.Domain.ViewModels.Exprnses;

namespace WebCourseWorkActual.Service.Interfaces
{
    public interface IExpensesService
    {
        //Task<IBaseResponse<IEnumerable<User>>> GetUsers();
        Task<IBaseResponse<Expenses>> Create(NewExpenseViewModel model, int checkId);

        //BaseResponse<Dictionary<int, string>> GetRoles();

        //Task<BaseResponse<IEnumerable<ExpenseCategoryViewModel>>> GetUsers();
        Task<BaseResponse<IEnumerable<Expenses>>> GetExpenses(int id);
        Task<BaseResponse<Expenses>> GetExpenseOnId(int expenseId);
        Task<IBaseResponse<bool>> DeleteExpense(int expenseId);
        Task<IBaseResponse<Expenses>> ChangeExpense(ChangeExpenseViewModel model, int expenseId, int checkId);
    }
}
