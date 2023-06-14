using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCourseWorkActual.Domain.Entity;
using WebCourseWorkActual.Domain.Response;
using WebCourseWorkActual.Domain.ViewModels.CheckIncomeCategory;
using WebCourseWorkActual.Domain.ViewModels.Incomes;
using WebCourseWorkActual.Domain.ViewModels.ExpeseCategory;
using WebCourseWorkActual.Domain.ViewModels.Exprnses;

namespace WebCourseWorkActual.Service.Interfaces
{
    public interface IIncomesService
    {
        //Task<IBaseResponse<IEnumerable<User>>> GetUsers();
        Task<IBaseResponse<Incomes>> Create(NewIncomeViewModel model, int checkId);

        //BaseResponse<Dictionary<int, string>> GetRoles();

        //Task<BaseResponse<IEnumerable<IncomeCategoryViewModel>>> GetUsers();
        Task<BaseResponse<IEnumerable<Incomes>>> GetIncomes(int id);
        Task<BaseResponse<Incomes>> GetIncomeOnId(int expenseId);
        Task<IBaseResponse<bool>> DeleteIncome(int expenseId);
        Task<IBaseResponse<Incomes>> ChangeIncome(ChangeIncomeViewModel model, int expenseId, int checkId);
    }
}
