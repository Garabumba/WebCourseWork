using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCourseWorkActual.DAL.Interfaces;
using WebCourseWorkActual.Domain.Entity;

namespace WebCourseWorkActual.DAL.Repositories
{
    public class ExpensesRepository : IBaseRepository<Expenses>
    {
        private readonly ApplicationDbContext _db;

        public ExpensesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<Expenses> GetAll()
        {
            return _db.Расходы;
        }

        public async Task Delete(Expenses entity)
        {
            _db.Расходы.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Create(Expenses entity)
        {
            await _db.Расходы.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Expenses> Update(Expenses entity)
        {
            _db.Расходы.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
