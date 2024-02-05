using Aplication.OverboardChess.Requests.CreateInvitationRequests;
using Aplication.OverboardChess.Requests.GetInvitationRequests;
using Aplication.OverboardChess.Requests.UpdateInvitationRequests;
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
        private readonly IMediator _mediator = mediator;

        [HttpGet("recived")]
        public async Task<ActionResult<GetRecivedInvitationsResponse>> GetRecivedInvitations()
        {
            var response = await _mediator.Send(new GetRecivedInvitationsRequest());
            return Ok(response);
        }

        [HttpPost("invite")]
        public async Task<IActionResult> InviteUser([FromBody] InviteUserBody body)
        {
            var request = new CreateInvitationRequest()
            {
                MeetingId = body.MeetingId,
                UserId = body.UserId,
            };
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPut("{id}/accept")]
        public async Task<IActionResult> AcceptInvitation([FromRoute] Guid id)
        {
            var request = new AcceptInvitationRequest
            {
                InvitationId = id,
            };
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPut("{id}/reject")]
        public async Task<IActionResult> RejectInvitation([FromRoute] Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
