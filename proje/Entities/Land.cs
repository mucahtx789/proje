using System.ComponentModel.DataAnnotations;

namespace proje.Entities
{
    public class Land
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        public int price { get; set; }

        public int LandSize { get; set; }
    }
}
