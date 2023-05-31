using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCourseWorkActual.Domain.Entity
{
    public class Check
    {
        public int Id { get; set; }
        public decimal Баланс { get; set; }
        public int IdПользователя { get; set; }
    }
}