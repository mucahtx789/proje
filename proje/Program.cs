using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using proje.Entities;
using System.Reflection;

namespace proje
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.AddDbContext<DataBaseContext>(opts =>
            {
                opts.UseSqlServer("SERVER=DESKTOP-VI5LI79;Database=proje;Trusted_Connection=True;TrustServerCertificate=True");

            });
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opts => {
                opts.Cookie.Name = "Emlak";
                opts.ExpireTimeSpan=TimeSpan.FromDays(1);
                opts.LoginPath = "/Login/Login";
                opts.LogoutPath = "/Login/LoginOut";
               // admin sayfasýna yetkisiz girþ opts.AccessDeniedPath = "/Home/AccessDenied";
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //Authentication çalýþtýr
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}