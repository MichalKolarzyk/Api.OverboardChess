using Application.OverboardChess.Repositories;
using Application.OverboardChess.Requests.CreateMeetingRequests;
using Domain.OverboardChess.Meetings;
using Http.OverboardChess.Controllers.MeetingControllers.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Http.OverboardChess.Controllers.MeetingControllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class MeetingController(IRepository<Meeting> meetingRepository, IMediator mediator, IHttpContextAccessor httpContextAccessor) : ControllerBase
    {
        private readonly IRepository<Meeting> _meetingRepository = meetingRepository;
        private readonly IMediator _mediator = mediator;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMeetingBody body)
        {

            var request = new CreateMeetingRequest(body.Title, body.Start, body.DurationHours, body.DurationMinutes);
            await _mediator.Send(request);
            return Ok();
        }

        [HttpGet]
        public async Task<List<Meeting>> GetAllMeetings()
        {
            var meetings = await _meetingRepository.GetListAsync((m) => true);

            return meetings;
        }
    }
}
