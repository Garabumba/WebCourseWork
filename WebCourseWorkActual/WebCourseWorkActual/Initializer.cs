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
            //services.AddScoped<IBaseRepository<Car>, CarRepository>();
            services.AddScoped<IBaseRepository<User>, UserRepository>();
            services.AddScoped<IBaseRepository<Check>, CheckRepository>();
            services.AddScoped<IBaseRepository<ExpenseCategory>, ExpenseCategoryRepository>();
            services.AddScoped<IBaseRepository<CheckExpenseCategory>, CheckExpenseCategoryRepository>();
            services.AddScoped<IBaseRepository<IncomeCategory>, IncomeCategoryRepository>();
            services.AddScoped<IBaseRepository<CheckIncomeCategory>, CheckIncomeCategoryRepository>();
            //services.AddScoped<IBaseRepository<Profile>, ProfileRepository>();
            //services.AddScoped<IBaseRepository<Basket>, BasketRepository>();
            //services.AddScoped<IBaseRepository<Order>, OrderRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            //services.AddScoped<ICarService, CarService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICheckService, CheckService>();
            services.AddScoped<IExpenseCategoryService, ExpenseCategoryService>();
            services.AddScoped<ICheckExpenseCategoryService, CheckExpenseCategoryService>();
            services.AddScoped<IIncomeCategoryService, IncomeCategoryService>();
            services.AddScoped<ICheckIncomeCategoryService, CheckIncomeCategoryService>();
            //services.AddScoped<IProfileService, ProfileService>();
            //services.AddScoped<IBasketService, BasketService>();
            //services.AddScoped<IOrderService, OrderService>();
        }
    }
}