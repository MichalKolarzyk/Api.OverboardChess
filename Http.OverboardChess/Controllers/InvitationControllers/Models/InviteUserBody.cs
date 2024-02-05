namespace Http.OverboardChess.Controllers.InvitationControllers.Models
{
    public class InviteUserBody
    {
        public Guid UserId { get; set; }
        public Guid MeetingId { get; set; }
    }
}
