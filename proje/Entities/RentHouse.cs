using System.ComponentModel.DataAnnotations;

namespace proje.Entities
{
    public class RentHouse
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        public int Price { get; set; }

        public int Size { get; set; }
        public int Monthly { get; set; }
        public string GardenCheck { get; set; }
        public Guid GardenId { get; set; }
        public int Floor { get; set; }
    }
}
