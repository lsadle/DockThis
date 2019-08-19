using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simplicity.Models.Auth;
using Simplicity.Services.Interfaces;

namespace Simplicity.Controllers
{
    [Route("Auth")]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        [Route("Login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var success = await _service.Login(model.Username, model.Password);

            if (success)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.UserData, model.Username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return View();
            }

            model.LoginFailed = true;
            return View(model);
        }

        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("CreateAccount")]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateAccount")]
        public async Task<IActionResult> CreateAccount(CreateAccountModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _service.UsernameIsTaken(model.Username))
                {
                    model.UsernameTaken = true;
                    return View(model);
                }
                
                await _service.CreateAccount(model.Username, model.Password);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}