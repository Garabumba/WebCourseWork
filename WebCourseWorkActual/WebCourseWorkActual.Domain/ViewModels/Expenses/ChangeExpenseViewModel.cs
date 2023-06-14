using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCourseWorkActual.Domain.ViewModels.Expenses
{
    public class ChangeExpenseViewModel
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "Укажите описание")]
        [MaxLength(20, ErrorMessage = "Описание должно иметь длину меньше 50 символов")]
        [MinLength(1, ErrorMessage = "Описание должно иметь длину больше 1 символа")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите категорию")]
        public string Category { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Укажите сумму")]
        [MaxLength(20, ErrorMessage = "Описание должно иметь длину меньше 50 символов")]
        [MinLength(1, ErrorMessage = "Описание должно иметь длину больше 1 символа")]
        public string Sum { get; set; }
    }
}
