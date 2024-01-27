using Aplication.OverboardChess.Meetings;
using Application.OverboardChess.Repositories;
using Http.OverboardChess.Controllers.MeetingControllers.Models;
using Microsoft.AspNetCore.Mvc;

namespace Http.OverboardChess.Controllers.MeetingControllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeetingController(IRepository<Meeting> meetingRepository) : ControllerBase
    {
        private readonly IRepository<Meeting> _meetingRepository = meetingRepository;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMeetingBody createMeetingBody)
        {
            var meeting = new Meeting(createMeetingBody.Title);

            await _meetingRepository.InsertAsync(meeting);

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
