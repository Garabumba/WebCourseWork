using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCourseWorkActual.Domain.ViewModels.User
{
	public class UserViewModel
	{
        public int Id { get; set; }
        public string Фамилия { get; set; }
        public string Имя { get; set; }
        public string Отчество { get; set; }
        public string Email { get; set; }
        public string Пароль { get; set; }
    }
}
