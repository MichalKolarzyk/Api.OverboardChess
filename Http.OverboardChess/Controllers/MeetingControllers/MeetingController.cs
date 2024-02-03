using Application.OverboardChess.Repositories;
using Application.OverboardChess.Requests.CreateMeetingRequests;
using Domain.OverboardChess.Meetings;
using Http.OverboardChess.Controllers.MeetingControllers.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Http.OverboardChess.Controllers.MeetingControllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeetingController(IRepository<Meeting> meetingRepository, IMediator mediator) : ControllerBase
    {
        private readonly IRepository<Meeting> _meetingRepository = meetingRepository;
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMeetingBody createMeetingBody)
        {
            var request = new CreateMeetingRequest(createMeetingBody.Title);
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
