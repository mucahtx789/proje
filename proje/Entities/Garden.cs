using System.ComponentModel.DataAnnotations;

namespace proje.Entities
{
    public class Garden
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        public int Size { get; set; }
        public string Properties { get; set; }
        public int PoolSize { get; set; }
    }
}
