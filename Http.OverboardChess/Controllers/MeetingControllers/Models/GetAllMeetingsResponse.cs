using Domain.OverboardChess.Meetings;

namespace Http.OverboardChess.Controllers.MeetingControllers.Models
{
    public class GetAllMeetingsResponse
    {
        public List<Meeting> Meetings { get; set; } = [];
    }
}
