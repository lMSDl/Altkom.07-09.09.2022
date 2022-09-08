using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace WebApi.Controllers
{
    [Route($"api/{nameof(ShoppingList)}s/{{parentId}}/Items")]
    [ApiController]
    public class ShoppingListItemsController : RudController<ShoppingListItem>
    {

        private IShoppingListService _parentService;
        private IShoppingListItemService _service;

        public ShoppingListItemsController(IShoppingListService parentService, IShoppingListItemService service) : base(service)
        {
            _parentService = parentService;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCollection(int parentId)
        {
            if (await _parentService.ReadAsync(parentId) == null)
                return NotFound();

            return Ok(await _service.ReadCollectionAsync(parentId));
        }


        [HttpPost]
        public async Task<IActionResult> Post(int parentId, ShoppingListItem entity)
        {
            if (await _parentService.ReadAsync(parentId) == null)
                return NotFound();

            var items = await _service.ReadCollectionAsync(parentId);
            if(items.Any(x => x.Name == entity.Name))
            {
                ModelState.AddModelError(nameof(ShoppingListItem.Name), "Name must be unique");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            entity = await _service.CreateAsync(parentId, entity);

            return CreatedAtAction(nameof(Get), new { Id = entity.Id }, entity);
        }
    }
}
