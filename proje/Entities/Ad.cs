using System.ComponentModel.DataAnnotations;

namespace proje.Entities
{
    public class Ad
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(50)]
        public string Properties { get; set; }
        [MaxLength(300)]
        public string TextBody { get; set; }
        public Guid AddressId { get; set; }
        public string SellerTelephone { get; set; }
        public string SalesType { get; set; }
        public Guid typeId { get; set; }
        public Guid userId { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;

    }
}
