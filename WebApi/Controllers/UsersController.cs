using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Authorize]
    public class UsersController : CrudController<User>
    {
        private AuthService _authService;

        public UsersController(ICrudService<User> service, AuthService authService) : base(service)
        {
            _authService = authService;
        }

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        public override async Task<IActionResult> Post(User entity)
        {
            await Task.Yield();
            //return StatusCode(StatusCodes.Status418ImATeapot);
            throw new Exception();
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login(User user)
        {
            var token = _authService.Authenticate(user.UserName, user.Password);
            if (token == null)
                return BadRequest();

            return Ok(token);
        }
    }
}
