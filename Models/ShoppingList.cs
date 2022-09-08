using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ShoppingList : Entity
    {
        public DateTime DateTime { get; set; }
        [Required]
        public string? Name { get; set; }

    }
}