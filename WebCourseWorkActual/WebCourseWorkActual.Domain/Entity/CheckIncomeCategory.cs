using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCourseWorkActual.Domain.Entity;

namespace WebCourseWorkActual.Domain.Entity
{
    public class CheckIncomeCategory
    {
        public int IdСчёта { get; set; }
        public int IdКатегорииДохода { get; set; }
        public Check? Check { get; set; }
        public IncomeCategory? IncomeCategory { get; set; }


    }
}