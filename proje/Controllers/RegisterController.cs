using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using proje.Entities;
using proje.Models;

namespace proje.Controllers
{
    public class RegisterController : Controller
    {
        private readonly DataBaseContext _dataBaseContext;

        public RegisterController(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }
        public IActionResult Register()
        {
          
            return View();//sayfa ilk açılış
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)//model geldiyse
            {
                //ToLower büyük küçük harf dikkat etmesi için bu email daha önce kullanılmış mı kontrol
                if (_dataBaseContext.Users.Any(x => x.Email.ToLower()==model.Email.ToLower())) 
                {
                    //hatayı yaz sayfayı bu modelle yenile
                    ModelState.AddModelError(model.Email, "Bu email adresi kullanılıyor!");
                    return View(model);
                }


                // kullanıcı için id oluştur
                Guid UserNewGuidId = Guid.NewGuid();
               
               //kayıt işlemi kontrol etme değişkeni
                int registerErrorControl=0;

                //şifrenin başına scü eklenip md5 formatında şifrelendi
                string saltedPassword = "scü" + model.Password;
                string passwordMd5 = saltedPassword.MD5();

                //kullanıcıyı bu id ve sayfadan gelen değerlerle kayıt et
                User user = new User() {Id= UserNewGuidId, Email=model.Email,Password=passwordMd5,Name=model.Name,LastName=model.lastName,Telephone=model.Telephone };
                _dataBaseContext.Users.Add(user);
               

                //sayfada kayıt tipi ne geldiyse o tabloya kullanıcı id ile beraber kayıt et
                if (model.Role == "Seller") { 
                    Seller seller = new Seller() { UserId = UserNewGuidId };
                    _dataBaseContext.Sellers.Add(seller);  

                }
                if (model.Role == "Customer") { Customer customer = new Customer() {UserId=UserNewGuidId ,Interest="boş"}; _dataBaseContext.Customers.Add(customer); }

                registerErrorControl = _dataBaseContext.SaveChanges();
            
                if (registerErrorControl == 0) { ModelState.AddModelError("", "Kayıt oluşturulamadı"); }//veritabanı kayıttda hata varsa validation-summury de bilgilendir
                else { ViewBag.message = "Kayıt Başarılı";  }//yoksa giriş sayfasına git RedirectToAction(LoginController);
            }

           
            return View(model);//hata varsa modeli geri gönderip view modeldeki hata mesajları yazıcak
        }

       
    }
}
