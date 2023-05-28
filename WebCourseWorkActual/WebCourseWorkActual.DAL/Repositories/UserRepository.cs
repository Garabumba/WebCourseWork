using Microsoft.EntityFrameworkCore;
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
    public class UserRepository : IBaseRepository<User>
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<User> GetAll()
        {
            return _db.Пользователь;
        }

        public async Task Delete(User entity)
        {
            _db.Пользователь.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Create(User entity)
        {
            await _db.Пользователь.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<User> Update(User entity)
        {
            _db.Пользователь.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
