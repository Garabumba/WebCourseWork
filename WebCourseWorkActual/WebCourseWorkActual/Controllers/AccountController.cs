﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebCourseWorkActual.Domain.Response;
using WebCourseWorkActual.Domain.ViewModels.Account;
using WebCourseWorkActual.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebCourseWorkActual.Domain.ViewModels.Check;

namespace WebCourseWorkActual.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ICheckService _checkService = null;
        
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Register(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Login(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        //[HttpGet]
        //public IActionResult DeleteAccount() => View();

        //[HttpPost]
        //public async Task<IActionResult> DeleteAccount(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var response = await _accountService.Register(model);
        //        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        //        {
        //            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
        //                new ClaimsPrincipal(response.Data));

        //            return RedirectToAction("Index", "Home");
        //        }
        //        ModelState.AddModelError("", response.Description);
        //    }
        //    return View(model);
        //}

        /*[HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.ChangePassword(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return Json(new { description = response.Description });
                }
            }
            var modelError = ModelState.Values.SelectMany(v => v.Errors);
            
            return StatusCode(StatusCodes.Status500InternalServerError, new { modelError.FirstOrDefault().ErrorMessage });
        }*/
    }
}