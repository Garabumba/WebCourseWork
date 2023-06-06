using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCourseWorkActual.DAL.Interfaces;
using WebCourseWorkActual.Domain.Entity;

namespace WebCourseWorkActual.DAL.Repositories
{
    public class CheckIncomeCategoryRepository : IBaseRepository<CheckIncomeCategory>
    {
        private readonly ApplicationDbContext _db;

        public CheckIncomeCategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<CheckIncomeCategory> GetAll()
        {
            return _db.Счёт_КатегорияДоходов;
        }

        public async Task Delete(CheckIncomeCategory entity)
        {
            _db.Счёт_КатегорияДоходов.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Create(CheckIncomeCategory entity)
        {
            await _db.Счёт_КатегорияДоходов.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<CheckIncomeCategory> Update(CheckIncomeCategory entity)
        {
            _db.Счёт_КатегорияДоходов.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
