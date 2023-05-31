using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCourseWorkActual.Domain.Entity;

namespace test3.Domain.Entity
{
    public class Check_ExpenseCategory
    {
        public Check Check { get; set; }
        public int CheckId { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
        public int ExpenseCategoryId { get; set; }
    }
}