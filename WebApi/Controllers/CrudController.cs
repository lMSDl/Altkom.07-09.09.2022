using Microsoft.AspNetCore.Authorization;
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
    public abstract class CrudController<T> : RudController<T> where T : Entity
    {
        private ICrudService<T> _service;

        protected CrudController(ICrudService<T> service) : base(service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Read")]
        //public Task<IEnumerable<ShoppingList>> Get()
        public virtual async Task<IActionResult> Get()
        {
            return Ok(await _service.ReadAsync());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post(T entity)
        {
            entity = await _service.CreateAsync(entity);

            return CreatedAtAction(nameof(Get), new { Id = entity.Id }, entity);
        }
    }
}
