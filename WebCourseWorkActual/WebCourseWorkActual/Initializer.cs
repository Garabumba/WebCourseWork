using WebCourseWorkActual.DAL.Interfaces;
using WebCourseWorkActual.DAL.Repositories;
using WebCourseWorkActual.Domain.Entity;
using WebCourseWorkActual.Service.Implementations;
using WebCourseWorkActual.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace WebCourseWorkActual
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<User>, UserRepository>();
            services.AddScoped<IBaseRepository<Check>, CheckRepository>();
            services.AddScoped<IBaseRepository<ExpenseCategory>, ExpenseCategoryRepository>();
            services.AddScoped<IBaseRepository<CheckExpenseCategory>, CheckExpenseCategoryRepository>();
            services.AddScoped<IBaseRepository<IncomeCategory>, IncomeCategoryRepository>();
            services.AddScoped<IBaseRepository<CheckIncomeCategory>, CheckIncomeCategoryRepository>();
            services.AddScoped<IBaseRepository<Expenses>, ExpensesRepository>();
            services.AddScoped<IBaseRepository<Incomes>, IncomesRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICheckService, CheckService>();
            services.AddScoped<IExpenseCategoryService, ExpenseCategoryService>();
            services.AddScoped<ICheckExpenseCategoryService, CheckExpenseCategoryService>();
            services.AddScoped<IIncomeCategoryService, IncomeCategoryService>();
            services.AddScoped<ICheckIncomeCategoryService, CheckIncomeCategoryService>();
            services.AddScoped<IExpensesService, ExpensesService>();
            services.AddScoped<IIncomesService, IncomesService>();
        }
    }
}