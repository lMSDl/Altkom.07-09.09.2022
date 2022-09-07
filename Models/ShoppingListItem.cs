using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ShoppingListItem : Entity
    {
        public int ShoppingListId { get; set; }
        public string? Name { get; set; }
        public bool Checked { get; set; }
    }
}
