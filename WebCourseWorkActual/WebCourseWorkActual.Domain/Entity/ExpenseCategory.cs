using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCourseWorkActual.Domain.Entity
{
    public class ExpenseCategory
    {
        public int Id { get; set; }
        public string Название { get; set; }
        public List<Check> Checks { get; set; } = new();
        public List<CheckExpenseCategory> CheckExpenseCategories { get; set; } = new();
    }
}