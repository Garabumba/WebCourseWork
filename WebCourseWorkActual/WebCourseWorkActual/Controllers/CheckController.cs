using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> GetChecks()
        {
            var response = await _checkService.GetChecks();
            return View(response.Data);
        }
    }
}
