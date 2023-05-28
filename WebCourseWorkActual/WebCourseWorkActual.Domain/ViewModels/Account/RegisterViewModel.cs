using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCourseWorkActual.Domain.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Укажите имя")]
        [MaxLength(20, ErrorMessage = "Имя должно иметь длину меньше 20 символов")]
        [MinLength(3, ErrorMessage = "Имя должно иметь длину больше 3 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите фамилию")]
        [MaxLength(20, ErrorMessage = "Фамилия должна иметь длину меньше 20 символов")]
        [MinLength(3, ErrorMessage = "Фамилия должна иметь длину больше 3 символов")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Укажите отчество")]
        [MaxLength(30, ErrorMessage = "Отчество должно иметь длину меньше 30 символов")]
        [MinLength(3, ErrorMessage = "Отчество должно иметь длину больше 3 символов")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Укажите email")]
        [MaxLength(50, ErrorMessage = "Email должен иметь длину меньше 50 символов")]
        [MinLength(3, ErrorMessage = "Email должен иметь длину больше 3 символов")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Укажите пароль")]
        [MinLength(6, ErrorMessage = "Пароль должен иметь длину больше 6 символов")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
    }
}
