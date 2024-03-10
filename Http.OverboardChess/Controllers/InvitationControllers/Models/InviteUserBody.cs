namespace Http.OverboardChess.Controllers.InvitationControllers.Models
{
    public class InviteUserBody
    {
        public string Username { get; set; } = string.Empty;
        public Guid MeetingId { get; set; }
    }
}
