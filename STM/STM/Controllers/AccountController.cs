using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STM.Models;
using STM.Models.Data;
using STM.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using STM.Infrastructure;

namespace STM.Controllers
{
    public class AccountController : Controller
    {
        private STM_DBContext db { get; set; }
        private ICryptography crypto { get; set; }

        public AccountController(STM_DBContext db, ICryptography crypto)
        {
            this.db = db;
            this.crypto = crypto;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                CUser user = await db.CUser.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == crypto.GetHash(model.Password));
                if (user != null)
                {
                    await Authenticate(model.Login);
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Error = "Некорректные логин и(или) пароль";
            }

            ViewBag.Error = ViewBag.Error == null ? "Необходимо заполнить все обязательные поля" : ViewBag.Error;
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                CUser user = await db.CUser.FirstOrDefaultAsync(u => u.Login == model.Login || u.Email == model.Email);
                if (user == null)
                {
                    db.CUser.Add(new CUser {Login = model.Login, Email = model.Email, Password = crypto.GetHash(model.Password), FirstName = model.FirstName, LastName = model.LastName });
                    await db.SaveChangesAsync();
                    await Authenticate(model.Login);

                    return RedirectToAction("Index", "Home");
                }

                string error = "Пользователь с таким" + user.Login == model.Login ? "логином" : "Email" + " уже существует";
                ModelState.AddModelError("", error);
            }

            ViewBag.Error = ViewBag.Error == null ? "Необходимо заполнить все обязательные поля" : ViewBag.Error;
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}