namespace Http.OverboardChess.Controllers.MeetingControllers.Models
{
    public class CreateMeetingBody
    {
        public string Title { get; set; } = string.Empty;
        public DateTime Start { get; set; }
        public int DurationHours {  get; set; }
        public int DurationMinutes { get; set; }
    }
}
