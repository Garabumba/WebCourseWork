using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCourseWorkActual.DAL.Interfaces;
using WebCourseWorkActual.Domain.Entity;

namespace WebCourseWorkActual.DAL.Repositories
{
    public class CheckExpenseCategoryRepository : IBaseRepository<CheckExpenseCategory>
    {
        private readonly ApplicationDbContext _db;

        public CheckExpenseCategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<CheckExpenseCategory> GetAll()
        {
            return _db.Счёт_КатегорияРасходов;
        }

        public async Task Delete(CheckExpenseCategory entity)
        {
            _db.Счёт_КатегорияРасходов.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Create(CheckExpenseCategory entity)
        {
            await _db.Счёт_КатегорияРасходов.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<CheckExpenseCategory> Update(CheckExpenseCategory entity)
        {
            _db.Счёт_КатегорияРасходов.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
