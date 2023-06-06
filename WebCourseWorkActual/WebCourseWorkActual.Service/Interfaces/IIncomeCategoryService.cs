using WebCourseWorkActual.Domain.Entity;
using WebCourseWorkActual.Domain.Response;
using WebCourseWorkActual.Domain.ViewModels.IncomeCategory;

namespace WebCourseWorkActual.Service.Interfaces
{
    public interface IIncomeCategoryService
    {
        //Task<IBaseResponse<IEnumerable<User>>> GetUsers();
        Task<IBaseResponse<IncomeCategory>> Create(NewIncomeCategoryViewModel model, int id);

        //BaseResponse<Dictionary<int, string>> GetRoles();

        //Task<BaseResponse<IEnumerable<IncomeCategoryViewModel>>> GetUsers();
        Task<BaseResponse<IEnumerable<IncomeCategory>>> GetIncomeCategories(int id);
        Task<IBaseResponse<bool>> DeleteIncomeCategory(string name, int checkId);
        Task<IBaseResponse<IncomeCategory>> ChangeIncomeCategory(ChangeIncomeCategoryViewModel model, int checkId, string oldName);
    }
}
