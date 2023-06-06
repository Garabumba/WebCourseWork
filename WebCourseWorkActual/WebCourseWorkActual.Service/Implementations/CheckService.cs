using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCourseWorkActual.DAL.Interfaces;
using WebCourseWorkActual.Domain.Entity;
using WebCourseWorkActual.Domain.Enum;
using WebCourseWorkActual.Domain.Response;
using WebCourseWorkActual.Domain.ViewModels.Check;
using WebCourseWorkActual.Service.Interfaces;

namespace WebCourseWorkActual.Service.Implementations
{
    public class CheckService : ICheckService
    {
        private readonly ILogger<CheckService> _logger;
        private readonly IBaseRepository<Check> _checkRepository;

        public CheckService(ILogger<CheckService> logger, IBaseRepository<Check> checkRepository)
        {
            _logger = logger;
            _checkRepository = checkRepository;
        }

        public async Task<Check> GetCheck(int id)
        {
            try
            {
                return await _checkRepository.GetAll().Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public async Task<Check> ChangeBalance(Check check)
        {
            try
            {
                return await _checkRepository.Update(check);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
