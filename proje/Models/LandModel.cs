using System.ComponentModel.DataAnnotations;

namespace proje.Models
{
    public class LandModel
    {
        [Required(ErrorMessage = "fiyat girilmesi zorunlu")]
        public int Price { get; set; }

        [Required(ErrorMessage = "alan girilmesi zorunlu")]
        public int Size { get; set; }
    }
}
