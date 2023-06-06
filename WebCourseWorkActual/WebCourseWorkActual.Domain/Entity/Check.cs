using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCourseWorkActual.Domain.Entity
{
    public class Check
    {
        public int Id { get; set; }
        public decimal Баланс { get; set; }
        public int IdПользователя { get; set; }
        public List<ExpenseCategory> ExpenseCategories { get; set; } = new();
        public List<IncomeCategory> IncomeCategories { get; set; } = new();
        public List<CheckExpenseCategory> CheckExpenseCategories { get; set; } = new();
        public List<CheckIncomeCategory> CheckIncomeCategories { get; set; } = new();
    }
}