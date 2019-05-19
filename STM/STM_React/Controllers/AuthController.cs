using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using System.Security.Claims;
using STM_React.Infrastructure;
using STM_React.Models.Data;
using STM_React.Services;
using STM_React.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace STM_React.Controllers
{
    public class AuthController : Controller
    {
        STM_DBContext _context;
        ICryptography _crypt;
        public AuthController(STM_DBContext context, ICryptography crypt)
        {
            _context = context;
            _crypt = crypt;
        }

        [HttpPost]
        public async Task Login()
        {
            LoginModel model = JsonConvert.DeserializeObject<LoginModel>(new System.IO.StreamReader(Request.Body).ReadLine());

            try
            {
                var identity = GetIdentity(model.Login, model.Password);
                if (identity == null)
                {
                    Response.StatusCode = 400;
                    await Response.WriteAsync("Некорректные логин и пароль");
                    return;
                }

                var response = GenerateToken(identity);
                Response.ContentType = "application/json";
                await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync(ex.Message);
                return;
            }
        }

        private object GenerateToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name,
                userId = identity.Claims.First(c => c.Type == "id").Value
            };

            return response;
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            CUser person = _context.CUser.FirstOrDefault(u => u.Login == username && u.Password == _crypt.GetHash(password));
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "user"),
                    new Claim("id", person.Id.ToString())
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }

        [HttpPost]
        public async Task Register()
        {
            RegisterModel model = JsonConvert.DeserializeObject<RegisterModel>(new System.IO.StreamReader(Request.Body).ReadLine());
            CUser user = _context.CUser.FirstOrDefault(u => u.Login == model.Login || u.Email == model.Email);
            if (user == null)
            {
                _context.CUser.Add(new CUser { Login = model.Login, Email = model.Email, Password = _crypt.GetHash(model.Password), FirstName = model.FirstName, LastName = model.LastName });
                await _context.SaveChangesAsync();

                var identity = GetIdentity(model.Login, model.Password);
                var response = GenerateToken(identity);

                Response.ContentType = "application/json";
                await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
                return;
            }
            else
            {
                Response.ContentType = "application/json";
                Response.StatusCode = 400;
                await Response.WriteAsync("Пользователь с таким" + user.Login == model.Login ? "логином" : "Email" + " уже существует");
                return;
            }
        }
    }
}