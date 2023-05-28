using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCourseWorkActual.Domain.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Фамилия { get; set; }
        public string Имя { get; set; }
        public string Отчество { get; set; }
        public string Email { get; set; }
        public string Пароль { get; set; }
        //public Check Счёт { get; set; }
        //public int IdСчёта { get; set; }
    }
}