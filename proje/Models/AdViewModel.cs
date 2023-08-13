using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace proje.Models
{
    public class AdViewModel
    {
        [Required(ErrorMessage = "Başlık girilmesi zorunlu")]
        [MaxLength(50, ErrorMessage = "Başlık  en fazla 50 karakter olmalı")]
        public string Title { get; set; }

        [MaxLength(80, ErrorMessage = "Başlık  en fazla 50 karakter olmalı")]
        public string? Properties { get; set; }
        public string? TextBody { get; set; }
        public string SalesType { get; set; }


        //adres
        [Required(ErrorMessage = "şehir girilmesi zorunlu")]
        [MaxLength(50, ErrorMessage = "şehir  en fazla 50 karakter olmalı")]
        public string Province { get; set; }
        [Required(ErrorMessage = "ilçe girilmesi zorunlu")]
        [MaxLength(50, ErrorMessage = "ilçe  en fazla 50 karakter olmalı")]
        public string District { get; set; }
        [Required(ErrorMessage = "adres girilmesi zorunlu")]
        [MaxLength(50, ErrorMessage = "adres  en fazla 50 karakter olmalı")]
        public string? AddressDetails { get; set; }


        //land,rentHouse,salesHouse

        [Required(ErrorMessage = "fiyat girilmesi zorunlu")]
        public int Price { get; set; }

        [Required(ErrorMessage = "alan girilmesi zorunlu")]
        public int Size { get; set; }


        public int Floor { get; set; }

        //rentHouse

        public int Monthly { get; set; }

        //salesHouse

        public int HouseAge { get; set; }
        public string? Renovation { get; set; }

        //Garden
        [DefaultValue("No")]
        public string GardenCheck { get; set; }

        public int GardenSize { get; set; }
        public string? GardenProperties { get; set; }

        public int PoolSize { get; set; }
        public string? sellerTelephone { get; set; }

        //ilan liste işlemi
        public string? CreatedAt { get; set; }
        public String? AddressId { get; set; }
        public String? typeId { get; set; }
        public String? Id { get; set;}
       
    }
}
