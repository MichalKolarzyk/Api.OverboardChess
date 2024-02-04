using Aplication.OverboardChess.Requests.CreateUserRequests;
using Aplication.OverboardChess.Requests.GetUserRequests;
using Http.OverboardChess.Controllers.UserControllers.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Http.OverboardChess.Controllers.UserControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<UserController>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserBody body)
        {
            var request = new RegisterUserRequest(body.Username, body.Password);
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserBody body)
        {
            var request = new LoginUserRequest(body.Username, body.Password);
            var token = await _mediator.Send(request);
            return Ok(token);
        }
    }
}
