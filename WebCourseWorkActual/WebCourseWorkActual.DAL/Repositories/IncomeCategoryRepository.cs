using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCourseWorkActual.DAL.Interfaces;
using WebCourseWorkActual.Domain.Entity;

namespace WebCourseWorkActual.DAL.Repositories
{
    public class IncomeCategoryRepository : IBaseRepository<IncomeCategory>
    {
        private readonly ApplicationDbContext _db;

        public IncomeCategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<IncomeCategory> GetAll()
        {
            return _db.КатегорияДоходов;
        }

        public async Task Delete(IncomeCategory entity)
        {
            _db.КатегорияДоходов.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Create(IncomeCategory entity)
        {
            await _db.КатегорияДоходов.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<IncomeCategory> Update(IncomeCategory entity)
        {
            _db.КатегорияДоходов.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
