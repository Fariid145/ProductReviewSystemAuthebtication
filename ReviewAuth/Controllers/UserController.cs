using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ProductReviewAuthentication.Models;
using ProductReviewAuthentication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProductReviewAuthentication.Controllers
{
    public class UserController : Controller
    {
        public IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(CreateUserViewModel model)
        {

            _userService.RegisterUser(model.Email, model.Name, model.Gender, model.Password);
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {



            User user = _userService.LoginUser(model.Email, model.Password);

            if (user == null) return View();


            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var props = new AuthenticationProperties();

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);


            return RedirectToAction("Index", "Dashboard");


        }

        [HttpGet]
        public IActionResult LogoutOption()
        {
            Guid userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            User user = _userService.FindUserById(userId);

            ViewBag.UserName = user.Name;
            return View();
        }

        
        public async Task<IActionResult> Logout()
        {
            
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }


    }
}
