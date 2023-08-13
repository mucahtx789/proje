using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using proje.Entities;
using proje.Models;
using System.Security.Claims;

namespace proje.Controllers
{
    public class LoginController : Controller
    {
        private readonly DataBaseContext _dataBaseContext;

        public LoginController(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //scü ekle md5 çevir
                string saltedPassword = "scü" + model.Password;
                string passwordMd5 = saltedPassword.MD5();

                User user = _dataBaseContext.Users.SingleOrDefault(x=> x.Email.ToLower()==model.Email.ToLower() && x.Password==passwordMd5);// mail şifre kontrol
                if (user != null)
                { 
                   // ViewBag.message = "Oturum açma başarılı"; 
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim("Id",user.Id.ToString()));
                    claims.Add(new Claim("Name",user.Name ?? string.Empty));
                    claims.Add(new Claim("EMail", user.Email));
                    claims.Add(new Claim("Role", "Customer"));
                    claims.Add(new Claim("Telephone", user.Telephone));

                    Seller seller = _dataBaseContext.Sellers.SingleOrDefault(x=>x.UserId==user.Id );
                    if (seller != null)
                    {
                        claims.RemoveAt(3);
                        claims.Add(new Claim("Role", "Seller"));
                    }

                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("Index","Home");
                }
                else { ModelState.AddModelError("", "mail yada şifre hatalı"); }//hata varsa validation-summury de bilgilendir

            }
            return View(model);
        }

        public IActionResult LoginOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
