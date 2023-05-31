using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCourseWorkActual.DAL.Interfaces;
using WebCourseWorkActual.Domain.Entity;

namespace WebCourseWorkActual.DAL.Repositories
{
    /*public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(User entity)
        {
            await _db.AddAsync(entity);
            _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(User entity)
        {
            //_db.Пользователь.Remove(entity);
            //_db.SaveChangesAsync();
            throw new NotImplementedException();
        }

        public async Task<User> Get(int id)
        {
            return await _db.Пользователь.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _db.Пользователь.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<List<User>> Select()
        {
            return await _db.Пользователь.ToListAsync();
        }
    }*/
    public class CheckRepository : IBaseRepository<Check>
    {
        private readonly ApplicationDbContext _db;

        public CheckRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<Check> GetAll()
        {
            return _db.Счёт;
        }

        public async Task Delete(Check entity)
        {
            _db.Счёт.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Create(Check entity)
        {
            await _db.Счёт.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Check> Update(Check entity)
        {
            _db.Счёт.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
