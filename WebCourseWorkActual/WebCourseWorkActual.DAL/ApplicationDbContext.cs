using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable(tb => tb.HasTrigger("AddCheck"));
        }

        public DbSet<User> Пользователь { get; set; }
        public DbSet<Check> Счёт { get; set; }
    }
}
