using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCourseWorkActual.Domain.ViewModels.Check
{
    public class CheckViewModel
    {
        public int Id { get; set; }
        public decimal Баланс { get; set; }
        public int IdПользователя { get; set; }
    }
}
