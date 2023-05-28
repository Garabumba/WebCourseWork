using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCourseWorkActual.Domain.Entity;

namespace WebCourseWorkActual.DAL
{
    //Для работы с БД
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<User> Пользователь { get; set; }
    }
}
