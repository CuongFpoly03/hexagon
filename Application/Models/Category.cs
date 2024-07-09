using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace hexagonXg.Application.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public bool IsActive { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
