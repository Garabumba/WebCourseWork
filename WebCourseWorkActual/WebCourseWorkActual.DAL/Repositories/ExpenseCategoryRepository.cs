using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCourseWorkActual.DAL.Interfaces;
using WebCourseWorkActual.Domain.Entity;

namespace WebCourseWorkActual.DAL.Repositories
{
    public class ExpenseCategoryRepository : IBaseRepository<ExpenseCategory>
    {
        private readonly ApplicationDbContext _db;

        public ExpenseCategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<ExpenseCategory> GetAll()
        {
            return _db.КатегорияРасходов;
        }

        public async Task Delete(ExpenseCategory entity)
        {
            _db.КатегорияРасходов.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Create(ExpenseCategory entity)
        {
            await _db.КатегорияРасходов.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<ExpenseCategory> Update(ExpenseCategory entity)
        {
            _db.КатегорияРасходов.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
