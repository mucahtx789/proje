using System.ComponentModel.DataAnnotations;

namespace proje.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Mail adresi girilmesi zorunlu")]
        [MaxLength(50, ErrorMessage = "Mail adresi en fazla 50 karakter olmalı")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre girilmesi zorunlu")]
        [MaxLength(50, ErrorMessage = "Şifre en fazla 50 karakter olmalı")]
        public string Password { get; set; }
    }
}
