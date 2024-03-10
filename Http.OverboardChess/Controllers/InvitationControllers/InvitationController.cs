using Aplication.OverboardChess.Abstractions.Repositories.InvitationRepositories.ViewModels;
using Aplication.OverboardChess.Requests.InvitationRequests;
using Http.OverboardChess.Controllers.InvitationControllers.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Http.OverboardChess.Controllers.InvitationControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvitationController(IMediator mediator) : ControllerBase
    {
        [HttpGet("recived")]
        public async Task<ActionResult<List<RecivedInvitationViewModel>>> GetRecivedInvitations()
        {
            var response = await mediator.Send(new GetRecivedInvitationsRequest());
            return Ok(response);
        }

        [HttpPost("invite")]
        public async Task<IActionResult> InviteUser([FromBody] InviteUserBody body)
        {
            var request = new CreateInvitationRequest()
            {
                MeetingId = body.MeetingId,
                Username = body.Username,
            };
            await mediator.Send(request);
            return Ok();
        }

        [HttpPut("{id}/accept")]
        public async Task<IActionResult> AcceptInvitation([FromRoute] Guid id)
        {
            var request = new AcceptInvitationRequest
            {
                InvitationId = id,
            };
            await mediator.Send(request);
            return Ok();
        }

        [HttpPut("{id}/reject")]
        public async Task<IActionResult> RejectInvitation([FromRoute] Guid id)
        {
            var request = new RejectInvitationRequest
            {
                InvitationId = id,
            };
            await mediator.Send(request);
            return Ok();
        }
    }
}
