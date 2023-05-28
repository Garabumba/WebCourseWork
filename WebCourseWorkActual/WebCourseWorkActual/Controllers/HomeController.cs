using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebCourseWorkActual.DAL.Interfaces;
using WebCourseWorkActual.Models;

namespace WebCourseWorkActual.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            //var response = await _userRepository.Select();
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