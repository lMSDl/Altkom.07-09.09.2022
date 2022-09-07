using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace WebApi.Controllers
{
    //Route - adnotacja pozwalająca na określenie adresu zasobów
    //[controller] - zastąpione zostanie nazwą klasy (bez dopisku Controller)
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingListsController : ControllerBase
    {
        private IShoppingListService _service;

        public ShoppingListsController(IShoppingListService service)
        {
            _service = service;
        }

        [HttpGet]
        //public Task<IEnumerable<ShoppingList>> Get()
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ReadAsync());
        }

        //api/ShoppingLists/15
        //{} - podajemy nazwę parametru metody pod którą ma być podstawiona wartość z adresu
        //: - możemy określić typ parametru oraz jego ograniczenia (https://docs.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2#route-constraints)
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _service.ReadAsync(id);
            if(entity == null)
                return NotFound();
            return Ok(entity);
        }
        [HttpGet("{name:alpha}")]
        public async Task<IActionResult> GetByName(string name)
        {
            await Task.Yield();
            return Ok(new ShoppingList() { Name = name});
        }

        [HttpPost]
        public async Task<IActionResult> Post(ShoppingList shoppingList)
        {
            shoppingList = await _service.CreateAsync(shoppingList);

            return CreatedAtAction(nameof(Get), new { Id = shoppingList.Id }, shoppingList);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, ShoppingList entity)
        {
            if (await _service.ReadAsync(id) == null)
                return NotFound();

            await _service.UpdateAsync(id, entity);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _service.ReadAsync(id) == null)
                return NotFound();
            await _service.DeleteAsync(id);

            //return Accepted();
            return NoContent();
        }
    }
}
