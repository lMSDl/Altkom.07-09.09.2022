using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace WebApi.Controllers
{
    public class UsersController : CrudController<User>
    {
        public UsersController(ICrudService<User> service) : base(service)
        {
        }

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        public override Task<IActionResult> Post(User entity)
        {
            throw new Exception();
        }
    }
}
