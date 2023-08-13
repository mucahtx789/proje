using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using proje.Entities;
using proje.Models;
using System;
using System.Dynamic;
using System.Security.Claims;
using System.Security.Cryptography;

namespace proje.Controllers
{
    public class AdController : Controller
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly IMapper _mapper;
      //  public string sqlquery = "SELECT * FROM Users INNER JOIN Sellers ON Users.Id=Sellers.UserId; ";
        public AdController(DataBaseContext dataBaseContext, IMapper mapper)
        {
            _dataBaseContext = dataBaseContext;
            _mapper = mapper;
        }
        public IActionResult AddAd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAd(AdViewModel model)
        {
            string telephone = User.FindFirst("Telephone").Value;
            Guid userID = new Guid(User.FindFirstValue("Id"));
            Guid gardenId = new Guid();
            if (model!=null)
            {
                ViewBag.message = "Model geldi";
                //adres ekleme
                Guid addressId = Guid.NewGuid();
                Address address = new Address() {Id=addressId,Province=model.Province,District=model.District,AddressDetails=model.AddressDetails};
               _dataBaseContext.Addresses.Add(address);

                if (model.GardenCheck == "Yes")
                {
                    //bagçe kaydı
                     gardenId = Guid.NewGuid();
                    Garden garden = new Garden() { Id = gardenId, Properties = model.GardenProperties,Size=model.GardenSize,PoolSize = model.PoolSize };
                    _dataBaseContext.Gardens.Add(garden);

                }
               
                //ev kaydı
                if (model.SalesType == "RentHouse")
                    {
                        Guid rentHouseId = Guid.NewGuid();
                        RentHouse rentHouse = new RentHouse() { Id = rentHouseId, Price = model.Price, Size = model.Size, Monthly = model.Monthly,Floor=model.Floor, GardenCheck = model.GardenCheck, GardenId = gardenId };
                        _dataBaseContext.RentHouses.Add(rentHouse);

                    ViewBag.message2 = "satış tipi:" + model.SalesType + " type id:" + rentHouseId.ToString() + " adres id:" + addressId.ToString() + " telefon:" + telephone;

                    //ilan oluşturma
                    Ad ad = new Ad() { typeId = rentHouseId, SalesType = model.SalesType, userId = userID, Title = model.Title, Properties = model.Properties, TextBody = model.TextBody, AddressId = addressId, SellerTelephone = telephone };
                    _dataBaseContext.Ads.Add(ad);
                    _dataBaseContext.SaveChanges();
                    ViewBag.message = "Kayıt başarılı";
                }
                    else if (model.SalesType == "SalesHouse")
                    {
                        Guid salesHouseId = Guid.NewGuid();
                        SalesHouse salesHouse = new SalesHouse() { Id = salesHouseId, Price = model.Price,Size=model.Size,HouseAge=model.HouseAge,Renovation=model.Renovation, GardenCheck = model.GardenCheck, GardenId = gardenId,Floor=model.Floor };
                        _dataBaseContext.SalesHouses.Add(salesHouse);

                        ViewBag.message2 = "satış tipi:" + model.SalesType + " type id:" + salesHouseId.ToString() + " adres id:" + addressId.ToString() + " telefon:" + telephone;

                        //ilan oluşturma
                        Ad ad = new Ad() { typeId = salesHouseId, SalesType = model.SalesType, userId = userID, Title = model.Title, Properties = model.Properties, TextBody = model.TextBody, AddressId = addressId, SellerTelephone = telephone };
                        _dataBaseContext.Ads.Add(ad);
                        _dataBaseContext.SaveChanges();
                        ViewBag.message = "Kayıt başarılı";

                    }
                
                //arsa kaydı
                if (model.SalesType == "Land")
                {
                    Guid landId = Guid.NewGuid();
                    Land land = new Land() {Id=landId,LandSize=model.Size,price=model.Price };
                    _dataBaseContext.Lands.Add(land);

                    ViewBag.message2 = "satış tipi:" + model.SalesType + " type id:" + landId.ToString() + " adres id:" + addressId.ToString() + " telefon:" + telephone;

                    //ilan oluşturma
                    Ad ad = new Ad() { typeId = landId, SalesType = model.SalesType, userId = userID, Title = model.Title, Properties = model.Properties, TextBody = model.TextBody, AddressId = addressId, SellerTelephone = telephone };
                    _dataBaseContext.Ads.Add(ad);
                    _dataBaseContext.SaveChanges();
                    ViewBag.message = "Kayıt başarılı";
                }

               


            }
          //  ViewBag.message="Model gelmedi";
            return View(model);
        }


        /*   public ViewResult adAdd2(AdViewModel model2,Guid typeId,Guid aId)
           {

               // oturum açma claim ile telefon numarası ve id
               string telephone = User.FindFirst("Telephone").ToString();
               Guid userID= new Guid(User.FindFirstValue("Id"));
               ViewBag.message2 = "satış tipi:" + model2.SalesType + " type id:" + typeId.ToString() + " adres id:"+aId.ToString()+" telefon:"+telephone;

               //ilan oluşturma
               Ad ad = new Ad() {typeId=typeId,SalesType=model2.SalesType ,userId=userID,Title=model2.Title,Properties=model2.Properties,TextBody=model2.TextBody,AddressId=aId,SellerTelephone=telephone};
               _dataBaseContext.Ads.Add(ad);

               _dataBaseContext.SaveChanges();
               ViewBag.message="Kayıt başarılı";
               return View(model2);
           }*/

        public IActionResult AdList()
        {
            return View();
           
        }
        public IActionResult _AdListPartial() 
        {

            /*   string connectionString = "SERVER=DESKTOP-VI5LI79;Database=proje;Trusted_Connection=True;TrustServerCertificate=True";
                string sqlquery = " select * from Ads inner join RentHouses on Ads.typeId=RentHouses.Id;";
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sqlquery, connection);
                connection.Open();
                SqlDataReader a = command.ExecuteReader();
                List<AdViewModel> model1 = new List<AdViewModel>();
                while (a.Read())
                {
                Guid aid= new Guid();
               
                    var details = new AdViewModel();
                    details.Title = a["Title"].ToString();
                    details.Properties = a["Properties"].ToString();
                    details.TextBody = a["TextBody"].ToString();
                    details.sellerTelephone = a["SellerTelephone"].ToString();
                    details.CreatedAt= a["CreatedAt"].ToString();
                    details.Price = Convert.ToInt32(a["Price"]);
                    details.Size = Convert.ToInt32(a["Size"]);
                    details.Mounthly = Convert.ToInt32(a["Monthly"]);
                     details.Floor = Convert.ToInt32(a["Floor"]);
                    model1.Add(details);
               
                }*/
           
            List<Ad> ads = _dataBaseContext.Ads.ToList();

             List<AdViewModel> model = ads.Select(x => _mapper.Map<AdViewModel>(x)).ToList();

            return PartialView("_AdListPartial", model);
        }
        //kiralık ev detay
        public IActionResult rentHouseDetails(string adresID, string typeID)
        {
            
          
            List<Address> adres=_dataBaseContext.Addresses.FromSqlRaw("SELECT * FROM Addresses where Id='"+ adresID + "'").ToList();
            List<RentHouse> renthouse = _dataBaseContext.RentHouses.FromSqlRaw("SELECT * FROM RentHouses where Id='" + typeID + "'").ToList();



            List<AdViewModel> model1=adres.Select(x=>_mapper.Map<AdViewModel>(x)).ToList();
            List<AdViewModel> model2 = renthouse.Select(x => _mapper.Map<AdViewModel>(x)).ToList();

            List<AdViewModel> model4 = new List<AdViewModel>();
            model4.Add(model1.FirstOrDefault());
            model4.Add(model2.FirstOrDefault());


            if (model4[1].GardenCheck == "Yes") { 
                List<Garden> gardens = _dataBaseContext.Gardens.FromSqlRaw("SELECT * FROM Gardens where Id='" + renthouse[0].GardenId + "'").ToList();
            model4[1].GardenSize = gardens[0].Size;
            model4[1].GardenProperties = gardens[0].Properties;
            model4[1].PoolSize = gardens[0].PoolSize;
            }

            return PartialView("_rentHouseDetails", model4);
        }
        //satılık ev detay
        public IActionResult saleHouseDetails(string adresID, string typeID)
        {


            List<Address> adres = _dataBaseContext.Addresses.FromSqlRaw("SELECT * FROM Addresses where Id='" + adresID + "'").ToList();
            List<SalesHouse> saleshouse = _dataBaseContext.SalesHouses.FromSqlRaw("SELECT * FROM SalesHouses where Id='" + typeID + "'").ToList();



            List<AdViewModel> model1 = adres.Select(x => _mapper.Map<AdViewModel>(x)).ToList();
            List<AdViewModel> model2 = saleshouse.Select(x => _mapper.Map<AdViewModel>(x)).ToList();

            List<AdViewModel> model4 = new List<AdViewModel>();
            model4.Add(model1.FirstOrDefault());
            model4.Add(model2.FirstOrDefault());


            if (model4[1].GardenCheck == "Yes")
            {
                List<Garden> gardens = _dataBaseContext.Gardens.FromSqlRaw("SELECT * FROM Gardens where Id='" + saleshouse[0].GardenId + "'").ToList();
                model4[1].GardenSize = gardens[0].Size;
                model4[1].GardenProperties = gardens[0].Properties;
                model4[1].PoolSize = gardens[0].PoolSize;
            }

            return PartialView("_saleHouseDetails", model4);
        }
        //arazi detay
        public IActionResult landDetails(string adresID, string typeID)
        {


            List<Address> adres = _dataBaseContext.Addresses.FromSqlRaw("SELECT * FROM Addresses where Id='" + adresID + "'").ToList();
            List<Land> land = _dataBaseContext.Lands.FromSqlRaw("SELECT * FROM Lands where Id='" + typeID + "'").ToList();



            List<AdViewModel> model1 = adres.Select(x => _mapper.Map<AdViewModel>(x)).ToList();

            model1[0].Price = land[0].price;
            model1[0].Size = land[0].LandSize;




            return PartialView("_landDetails", model1);
        }
    }
}
