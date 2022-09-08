using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    public class ShoppingListsController : CrudController<ShoppingList>
    {

        public ShoppingListsController(IShoppingListService service) : base(service)
        {
        }

        [HttpGet("{name:alpha}")]
        public async Task<IActionResult> GetByName(string name)
        {
            await Task.Yield();
            return Ok(new ShoppingList() { Name = name });
        }

        [ProducesResponseType(typeof(IEnumerable<ShoppingList>), 200)]
        public override Task<IActionResult> Get()
        {
            return base.Get();
        }
    }
}
