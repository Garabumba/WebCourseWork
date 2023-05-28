using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test3.Domain.Entity
{
    public class Check_IncomeCategory
    {
        public Check Check { get; set; }
        public int CheckId { get; set; }
        public IncomeCategory IncomeCategory { get; set; }
        public int IncomeCategoryId { get; set; }


    }
}