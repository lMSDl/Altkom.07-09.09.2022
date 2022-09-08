using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    public abstract class RudController<T> : ControllerBase where T : Entity
    {
        private IRudService<T> _service;

        protected RudController(IRudService<T> service)
        {
            _service = service;
        }

        //api/ShoppingLists/15
        //{} - podajemy nazwę parametru metody pod którą ma być podstawiona wartość z adresu
        //: - możemy określić typ parametru oraz jego ograniczenia (https://docs.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2#route-constraints)

        [HttpGet("/api/[controller]/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _service.ReadAsync(id);
            if (entity == null)
                return NotFound();
            return Ok(entity);
        }


        [HttpPut("/api/[controller]/{id}")]
        [Authorize(Roles = "Edit")]
        public async Task<IActionResult> Put(int id, T entity)
        {
            if (await _service.ReadAsync(id) == null)
                return NotFound();

            await _service.UpdateAsync(id, entity);

            return NoContent();
        }


        [HttpDelete("/api/[controller]/{id}")]
        [Authorize(Roles = nameof(Roles.Delete))] // \
                                                    //  => adnotacje jedna pod drugą - potrzebujemy obydwu uprawnień
        [Authorize(Roles = nameof(Roles.Admin))]  // /
        //[Authorize(Roles = "Delete, Admin")] //role po przecinku - potrzebujemy jednej z nich
        //[Authorize(Roles = $"{nameof(Roles.Admin)}, {nameof(Roles.Admin)}")]
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
