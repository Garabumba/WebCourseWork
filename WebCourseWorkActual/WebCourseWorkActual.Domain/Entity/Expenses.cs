using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test3.Domain.Entity
{
    public class Expenses
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Sum { get; set; }
        public DateTime Date { get; set; }
        public ExpenseCategory Category { get; set; }
        public int ExpenseCategoryId { get; set; }
        public Check Check { get; set; }
        public int CheckId { get; set; }
    }
}