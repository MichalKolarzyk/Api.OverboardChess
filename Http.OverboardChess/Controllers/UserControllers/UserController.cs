using Aplication.OverboardChess.Requests.UserRequests;
using Http.OverboardChess.Controllers.UserControllers.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Http.OverboardChess.Controllers.UserControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {

        [HttpPost("loginWithEmail")]
        public async Task<IActionResult> LoginWithEmail([FromBody] LoginWithEmailBody body)
        {
            var request = new LoginWithEmailRequest(body.Email);
            await mediator.Send(request);
            return Ok();
        }

        [HttpPost("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailBody body)
        {
            var request = new ConfirmEmailRequest(body.Email, body.Code);
            var token = await mediator.Send(request);
            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserBody body)
        {
            var request = new RegisterUserRequest(body.Username, body.Password);
            await mediator.Send(request);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserBody body)
        {
            var request = new LoginUserRequest(body.Username, body.Password);
            var token = await mediator.Send(request);
            return Ok(token);
        }
    }
}
