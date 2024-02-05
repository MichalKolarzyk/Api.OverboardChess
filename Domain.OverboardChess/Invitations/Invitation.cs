using Domain.OverboardChess.Base;
using Domain.OverboardChess.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
