using Microsoft.AspNetCore.Mvc;
using WebCourseWorkActual.DAL.Interfaces;
using WebCourseWorkActual.Domain.Entity;
using WebCourseWorkActual.Service.Interfaces;

namespace WebCourseWorkActual.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICheckService _checkService;

		public UserController(IUserService userService, ICheckService checkService)
		{
			_userService = userService;
			_checkService = checkService;
		}

		[HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _userService.GetUsers();
            return View(response.Data);
        }
        
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            int userId = int.Parse(User.Identity.Name);
            //var response = await _userService.GetUsers();
            var user = await _userService.GetUser(userId);
            var check = await _checkService.GetCheck(userId);
            General info = new General();
            info.User = user;
            info.Check = check;
            
            return View(info);
        }
    }
}
