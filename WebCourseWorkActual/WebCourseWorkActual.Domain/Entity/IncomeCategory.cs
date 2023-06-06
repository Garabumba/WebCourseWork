using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCourseWorkActual.Domain.Entity
{
    public class IncomeCategory
    {
        public int Id { get; set; }
        public string Название { get; set; }
        public List<Check> Checks { get; set; } = new();
        public List<CheckIncomeCategory> CheckIncomeCategories { get; set; } = new();
    }
}