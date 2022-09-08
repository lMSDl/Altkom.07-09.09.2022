using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Xml.Serialization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var parent = new Parent();
            parent.Name = "Parent";
            parent.DateTime = DateTime.Now;
            parent.Children = new List<Child> { new Child { Name = "Child1", Parent = parent }, new Child { Name = "Child2", Parent = parent } };


            return Ok(parent);

        }

        [HttpGet("children")]
        public IActionResult GetChildren()
        {
            var parent = new Parent();
            parent.Name = "Parent";
            parent.DateTime = DateTime.Now;
            var children = new List<Child> { new Child { Name = "Child1", Parent = parent }, new Child { Name = "Child2", Parent = parent } };


            return Ok(children);

        }
    }
}
