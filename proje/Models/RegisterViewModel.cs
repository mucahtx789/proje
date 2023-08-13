using System.ComponentModel.DataAnnotations;

namespace proje.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Mail adresi girilmesi zorunlu")]
        [MaxLength(50, ErrorMessage = "Mail adresi en fazla 50 karakter olmalı")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre girilmesi zorunlu")]
        [MaxLength(50, ErrorMessage = "Şifre en fazla 50 karakter olmalı")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre tekrar girilmesi zorunlu")]
        [Compare(nameof(Password), ErrorMessage = "Girilen şifreler aynı olmalı")]
        public string Password2 { get; set; }

        [MaxLength(30, ErrorMessage = "Ad adresi en fazla 30 karakter olmalı")]
        public string Name { get; set; }
        [MaxLength(30, ErrorMessage = "Soyad adresi en fazla 30 karakter olmalı")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Telefon numarası girilmesi zorunlu")]
        public string Telephone { get; set; }

        public string Role { get; set; }
       public string?  Id  { get; set; }
    }
}
