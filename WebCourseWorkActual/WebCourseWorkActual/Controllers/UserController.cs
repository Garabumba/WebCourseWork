using Microsoft.AspNetCore.Mvc;
using WebCourseWorkActual.DAL.Interfaces;
using WebCourseWorkActual.Service.Interfaces;

namespace WebCourseWorkActual.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _userService.GetUsers();
            return View(response.Data);
        }
    }
}
