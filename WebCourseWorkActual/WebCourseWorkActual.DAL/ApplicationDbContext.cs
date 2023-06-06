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

        public DbSet<User> Пользователь { get; set; }
        public DbSet<Check> Счёт { get; set; }
        public DbSet<ExpenseCategory> КатегорияРасходов { get; set; }
        public DbSet<CheckExpenseCategory> Счёт_КатегорияРасходов { get; set; }
        public DbSet<IncomeCategory> КатегорияДоходов { get; set; }
        public DbSet<CheckIncomeCategory> Счёт_КатегорияДоходов { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable(tb => tb.HasTrigger("AddCheck"));
            //modelBuilder.Entity<CheckExpenseCategory>(entity =>
            //{
            //    //entity.HasNoKey();
            //    //entity.ToTable("Счёт_КатегорияРасходов");
            //});
            modelBuilder
                .Entity<Check>()
                .HasMany(c => c.ExpenseCategories)
                .WithMany(s => s.Checks)
                .UsingEntity<CheckExpenseCategory>(
                   j => j
                    .HasOne(pt => pt.ExpenseCategory)
                    .WithMany(t => t.CheckExpenseCategories)
                    .HasForeignKey(pt => pt.IdКатегорииРасхода),
                   j => j
                    .HasOne(pt => pt.Check)
                    .WithMany(p => p.CheckExpenseCategories)
                    .HasForeignKey(pt => pt.IdСчёта)
                );

            modelBuilder
                .Entity<Check>()
                .HasMany(c => c.IncomeCategories)
                .WithMany(s => s.Checks)
                .UsingEntity<CheckIncomeCategory>(
                   j => j
                    .HasOne(pt => pt.IncomeCategory)
                    .WithMany(t => t.CheckIncomeCategories)
                    .HasForeignKey(pt => pt.IdКатегорииДохода),
                   j => j
                    .HasOne(pt => pt.Check)
                    .WithMany(p => p.CheckIncomeCategories)
                    .HasForeignKey(pt => pt.IdСчёта)
                );
        }
    }
}
