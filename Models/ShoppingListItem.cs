using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ShoppingListItem : Entity
    {
        [Range(1, 120)]
        public int ShoppingListId { get; set; }
        [MinLength(1)]
        public string? Name { get; set; }
        public bool Checked { get; set; }
    }
}
