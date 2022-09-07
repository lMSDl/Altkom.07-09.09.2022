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
        public Task<IEnumerable<ShoppingList>> Get()
        {
            return _service.ReadAsync();
        }

        //api/ShoppingLists/15
        //{} - podajemy nazwę parametru metody pod którą ma być podstawiona wartość z adresu
        //: - możemy określić typ parametru oraz jego ograniczenia (https://docs.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2#route-constraints)
        [HttpGet("{id:int}")]
        public Task<ShoppingList?> Get(int id)
        {
            return _service.ReadAsync(id);
        }
        [HttpGet("{name:alpha}")]
        public Task<ShoppingList?> GetByName(string name)
        {
            return Task.FromResult(new ShoppingList() { Name = name});
        }
    }
}
