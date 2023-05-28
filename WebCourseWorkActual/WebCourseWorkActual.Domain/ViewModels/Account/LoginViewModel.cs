using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCourseWorkActual.Domain.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите email")]
        [MaxLength(50, ErrorMessage = "Имя должно иметь длину меньше 50 символов")]
        [MinLength(3, ErrorMessage = "Имя должно иметь длину больше 3 символов")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
