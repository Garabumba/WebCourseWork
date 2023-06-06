using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCourseWorkActual.Domain.ViewModels.IncomeCategory
{
    public class NewIncomeCategoryViewModel
    {
        [Required(ErrorMessage = "Укажите название")]
        [MaxLength(20, ErrorMessage = "Название должно иметь длину меньше 50 символов")]
        [MinLength(1, ErrorMessage = "Название должно иметь длину больше 1 символа")]
        public string Name { get; set; }
    }
}
