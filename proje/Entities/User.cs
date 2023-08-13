using System.ComponentModel.DataAnnotations;

namespace proje.Entities
{
    public class User
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string Telephone { get; set; }
    }
}
