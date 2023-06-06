using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCourseWorkActual.Domain.Entity;

namespace WebCourseWorkActual.Domain.Entity
{
    public class CheckExpenseCategory
    {
        public int IdСчёта { get; set; }
        public int IdКатегорииРасхода { get; set; }
        public Check? Check { get; set; }
        public ExpenseCategory? ExpenseCategory { get; set; }
    }
}