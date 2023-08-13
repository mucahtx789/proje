using System.ComponentModel.DataAnnotations;

namespace proje.Entities
{
    public class Admin
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Mission { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public Guid AdListId { get; set; }

    }
}
