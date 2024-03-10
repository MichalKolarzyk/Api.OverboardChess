using Aplication.OverboardChess.Abstractions;
using Aplication.OverboardChess.Abstractions.Repositories.MeetingRepositories;
using Aplication.OverboardChess.Abstractions.Repositories.MeetingRepositories.ViewModels;
using Aplication.OverboardChess.Providers;
using Aplication.OverboardChess.Requests.MeetingRequests;
using Application.OverboardChess.Requests.MeetingRequests;
using Http.OverboardChess.Controllers.MeetingControllers.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Http.OverboardChess.Controllers.MeetingControllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MeetingController(IMeetingRepository meetingRepository, ICurrentIdentity currentIdentity, IMediator mediator, IDateTimeProvider dateTimeProvider) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMeetingBody body)
        {

            var request = new CreateMeetingRequest(body.Title, body.Start, body.DurationHours, body.DurationMinutes);
            await mediator.Send(request);
            return Ok();
        }

        [HttpGet("MeetingsWhereUserParticipate")]
        public async Task<List<MeetingWithUserViewModel>> GetMeetingsWhereUserParticipate()
        {
            return await meetingRepository.GetMeetingsWhereUserParticipate(currentIdentity.GetUserId(), dateTimeProvider.Now);
        }

        [HttpGet("UserOwnerMeetings")]
        public async Task<List<MeetingWithUserViewModel>> GetUserOwnerMeetings()
        {
            return await meetingRepository.GetUserOwnerMeetings(currentIdentity.GetUserId(), dateTimeProvider.Now);
        }

        [HttpGet("FindMeetings")]
        public async Task<List<MeetingWithUserViewModel>> FindMeetings([FromQuery] int skip, [FromQuery] int take)
        {
            return await meetingRepository.FindMeetings(currentIdentity.GetUserId(), dateTimeProvider.Now, skip, take);
        }

        [HttpGet("{meetingId}")]
        public async Task<GetMeetingResponse> GetMeeting([FromRoute] Guid meetingId)
        {
            return await mediator.Send(new GetMeetingRequest(meetingId));
        }


        [HttpPost("{meetingId}/join")]
        public async Task<ActionResult> Join([FromRoute] Guid meetingId)
        {
            await mediator.Send(new JoinMeetingRequest(meetingId));
            return Ok();
        }

        [HttpDelete("{meetingId}/delete")]
        public async Task<ActionResult> Delete([FromRoute] Guid meetingId)
        {
            await mediator.Send(new DeleteMeetingRequest(meetingId));
            return Ok();
        }
    }
}
