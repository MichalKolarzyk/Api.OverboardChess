using Domain.OverboardChess.Users;
using Utilities.OverboardChess.DomainBase;

namespace Domain.OverboardChess.Invitations
{
    public class Invitation : AggregateRoot
    {
        public Guid MeetingId { get; set; }
        public Guid OwnerUserId { get; set; }
        public Guid InvitedUserId { get; set; }
        public InvitationState State { get; set; }

        public Invitation() { }

        public Invitation(Guid meetingId, Guid userOwner, Guid invitedUser)
        {
            MeetingId = meetingId;
            OwnerUserId = userOwner;
            InvitedUserId = invitedUser;
            State = InvitationState.Created;
        }

        public void Accept(User requestedBy)
        {
            if (requestedBy.Id != InvitedUserId)
                throw new Exception("Only invited user can accept invitation.");

            if (State != InvitationState.Created)
                throw new Exception("Invitation has been already accepted or rejected");

            State = InvitationState.Accepted;
        }

        public void Reject(User requestedBy)
        {
            if (requestedBy.Id != InvitedUserId)
                throw new Exception("Only invited user can reject invitation.");

            State = InvitationState.Rejected;
        }
    }

    public enum InvitationState
    {
        Created,
        Accepted,
        Rejected,
    }
}
