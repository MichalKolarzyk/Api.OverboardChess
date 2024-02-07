using Aplication.OverboardChess.Abstractions.Repositories;
using Aplication.OverboardChess.Abstractions.Repositories.MeetingRepositories;
using Aplication.OverboardChess.Abstractions.Repositories.MeetingRepositories.ViewModels;
using Application.OverboardChess.Requests.CreateMeetingRequests;
using Domain.OverboardChess.Meetings;
using Http.OverboardChess.Controllers.MeetingControllers.Models;
using Infrastructure.OverboardChess.Database;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Http.OverboardChess.Controllers.MeetingControllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class MeetingController(IMeetingRepository meetingRepository, IMediator mediator, IHttpContextAccessor httpContextAccessor) : ControllerBase
    {
        private readonly IMeetingRepository _meetingRepository = meetingRepository;
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMeetingBody body)
        {

            var request = new CreateMeetingRequest(body.Title, body.Start, body.DurationHours, body.DurationMinutes);
            await _mediator.Send(request);
            return Ok();
        }

        [HttpGet]
        public async Task<List<MeetingWithUserViewModel>> GetAllMeetings()
        {
            return await _meetingRepository.GetMeetingWithUserViewModels(m => true);
        }
    }
}
