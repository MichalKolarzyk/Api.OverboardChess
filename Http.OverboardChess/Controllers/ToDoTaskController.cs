using Microsoft.AspNetCore.Mvc;

namespace Http.OverboardChess.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoTaskController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create()
        {
            return Ok();
        }
    }
}
