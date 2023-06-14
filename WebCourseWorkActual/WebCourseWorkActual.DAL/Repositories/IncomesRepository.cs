using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCourseWorkActual.DAL.Interfaces;
using WebCourseWorkActual.Domain.Entity;

namespace WebCourseWorkActual.DAL.Repositories
{
    public class IncomesRepository : IBaseRepository<Incomes>
    {
        private readonly ApplicationDbContext _db;

        public IncomesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<Incomes> GetAll()
        {
            return _db.Доходы;
        }

        public async Task Delete(Incomes entity)
        {
            _db.Доходы.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Create(Incomes entity)
        {
            await _db.Доходы.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Incomes> Update(Incomes entity)
        {
            _db.Доходы.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
