using Microsoft.AspNetCore.Mvc;
using WebCourseWorkActual.Domain.Entity;
using WebCourseWorkActual.Service.Interfaces;

namespace WebCourseWorkActual.Controllers
{
    public class CheckController : Controller
    {
        private readonly ICheckService _checkService;

        public CheckController(ICheckService checkService)
        {
            _checkService = checkService;
        }

        [HttpPost]
        public async Task<Check> ChangeBalance(decimal newValue)
        {
            int userId = int.Parse(User.Identity.Name);
            var response = await _checkService.GetCheck(userId);
            Check c = response;
            c.Баланс = newValue;
            return await _checkService.ChangeBalance(c);
            //return View(newResponse);//response.Data);
        }
    }
}
