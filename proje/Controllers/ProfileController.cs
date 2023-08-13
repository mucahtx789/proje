using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NETCore.Encrypt.Extensions;
using proje.Entities;
using proje.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace proje.Controllers
{
    public class ProfileController : Controller
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly IMapper _mapper;
        public ProfileController(DataBaseContext dataBaseContext, IMapper mapper)
        {
            _dataBaseContext = dataBaseContext;
            _mapper = mapper;
        }

        public IActionResult Profile()
        {
            Guid userID = new Guid(User.FindFirstValue("Id"));
            User user = _dataBaseContext.Users.SingleOrDefault(x => x.Id == userID);
            ViewData["Name"] = user.Name;
            ViewData["LastName"] = user.LastName;

           
           
            return View();
        }

        [HttpPost]
        public IActionResult ProfileChangeNameLastName([StringLength(50)] string? Name, [StringLength(50)] string? LastName)
        {
            if (ModelState.IsValid)
            {
                Guid userID = new Guid(User.FindFirstValue("Id"));
                User user = _dataBaseContext.Users.SingleOrDefault(x => x.Id == userID);
                user.Name = Name;
                user.LastName = LastName;
                _dataBaseContext.SaveChanges();
                ViewData["SaveCheck1"] = "Değişiklik kayıt edildi.";
                return RedirectToAction(nameof(Profile));
            }
            
            return View("Profile");
        }
        [HttpPost]
        public IActionResult ProfileChangePassword([Required][MaxLength(50)] string password)
        {
            if (ModelState.IsValid)
            {
                Guid userID = new Guid(User.FindFirstValue("Id"));
                User user = _dataBaseContext.Users.SingleOrDefault(x => x.Id == userID);

                string saltedPassword = "scü" + password;
                string passwordMd5 = saltedPassword.MD5();
                user.Password = passwordMd5;
                _dataBaseContext.SaveChanges();
                ViewData["SaveCheck"] = "Şifre Değişti";

            }
           
            return View("Profile");
        }

        public IActionResult _AdListPartial()
        {
            Guid userID = new Guid(User.FindFirstValue("Id"));
            List<Ad> ads = _dataBaseContext.Ads.FromSqlRaw("SELECT * FROM Ads where userId='" + userID + "'").ToList();
            List<AdViewModel> model = ads.Select(x => _mapper.Map<AdViewModel>(x)).ToList();

            return PartialView("_AdList", model);
        }
        public IActionResult DeleteAd(string adid)
        {


            Ad ad = _dataBaseContext.Ads.FromSqlRaw("SELECT * FROM Ads where Id='" + adid + "'").FirstOrDefault();


            Address address= _dataBaseContext.Addresses.FromSqlRaw("SELECT * FROM Addresses where Id='" + ad.AddressId + "'").FirstOrDefault();
            _dataBaseContext.Addresses.Remove(address);

            if(ad.SalesType== "RentHouse")
            {
                RentHouse renthouse = _dataBaseContext.RentHouses.FromSqlRaw("SELECT * FROM RentHouses where Id='" + ad.typeId + "'").FirstOrDefault(); 
                if (renthouse.GardenCheck == "Yes") 
                {
                    Garden garden= _dataBaseContext.Gardens.FromSqlRaw("SELECT * FROM Gardens where Id='" + renthouse.GardenId + "'").FirstOrDefault();
                    _dataBaseContext.Gardens.Remove(garden);
                }
                _dataBaseContext.RentHouses.Remove(renthouse);
                

            }
            else if (ad.SalesType == "SalesHouse") 
            {
                SalesHouse saleshouse = _dataBaseContext.SalesHouses.FromSqlRaw("SELECT * FROM SalesHouses where Id='" + ad.typeId + "'").FirstOrDefault();

                if (saleshouse.GardenCheck == "Yes") 
                {
                    Garden garden = _dataBaseContext.Gardens.FromSqlRaw("SELECT * FROM Gardens where Id='" + saleshouse.GardenId + "'").FirstOrDefault();
                    _dataBaseContext.Gardens.Remove(garden);

                }
                _dataBaseContext.SalesHouses.Remove(saleshouse);

            }

            else if (ad.SalesType == "Land") { _dataBaseContext.Lands.FromSqlRaw("Delete  From Lands where Id='" + ad.typeId + "' ; "); }

            _dataBaseContext.Ads.Remove(ad);
            _dataBaseContext.SaveChanges();
            return Profile();

        }
    }
}
