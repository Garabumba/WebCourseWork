using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCourseWorkActual.Domain.Entity;

namespace WebCourseWorkActual.DAL.Interfaces
{
    //функционал для конкретной таблицы
    public interface IUserRepository : IBaseRepository<User>
    {
        //докидываем новые методы, если нужны
        Task<User> GetByEmail(string email);
    }
}
