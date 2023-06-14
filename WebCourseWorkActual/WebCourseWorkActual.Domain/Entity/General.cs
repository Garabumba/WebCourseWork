using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCourseWorkActual.Domain.Entity
{
    public class General
    {
        public User User { get; set; }
        public Check Check { get; set; }
        public List<ExpenseCategory> ExpenseCategories { get; set; }
        public List<IncomeCategory> IncomeCategories { get; set; }
        public List<Expenses> Expenses { get; set; }
        public List<Incomes> Incomes { get; set; }
    }
}
