using System.ComponentModel.DataAnnotations;

namespace proje.Entities
{
    public class Address
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Province { get; set; }
        [MaxLength(50)]
        public string District { get; set; }
        [MaxLength(300)]
        public string AddressDetails { get; set; }
    }
}
