using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebCourseWorkActual.DAL.Interfaces;
using WebCourseWorkActual.Models;
using WebCourseWorkActual.Service.Interfaces;

namespace WebCourseWorkActual.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICheckService _checkService;

        public HomeController(ICheckService checkService)
        {
            _checkService = checkService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var response = await _userRepository.Select();
            if (User.Identity.IsAuthenticated)
            {
                var response = await _checkService.GetCheck(int.Parse(User.Identity.Name));
                return View(response);
            }
            else
                return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}