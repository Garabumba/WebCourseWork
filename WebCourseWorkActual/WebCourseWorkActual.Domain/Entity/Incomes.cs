using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCourseWorkActual.Domain.Entity;

namespace WebCourseWorkActual.Domain.Entity
{
    public class Incomes
    {
        public int Id { get; set; }
        public string Описание { get; set; }
        public decimal Сумма { get; set; }
        public DateTime Дата { get; set; }
        public int IdКатегорииДохода { get; set; }
        public int IdСчёта { get; set; }
    }
}