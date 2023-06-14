using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCourseWorkActual.Domain.ViewModels.Expenses
{
    public class ExpenseViewModel
    {
        public int Id { get; set; }
        public string Описание { get; set; }
        public decimal Сумма { get; set; }
        public DateTime Дата { get; set; }
        public int IdКатегорииРасхода { get; set; }
        public int IdСчёта { get; set; }
    }
}
