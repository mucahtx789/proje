using System.ComponentModel.DataAnnotations;

namespace proje.Entities
{
    public class Customer
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [MaxLength(80)]
        public string Interest { get; set; }
    }
}
