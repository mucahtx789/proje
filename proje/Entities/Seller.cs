using System.ComponentModel.DataAnnotations;

namespace proje.Entities
{
    public class Seller
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public Guid AdListId { get; set; }
    }
}
