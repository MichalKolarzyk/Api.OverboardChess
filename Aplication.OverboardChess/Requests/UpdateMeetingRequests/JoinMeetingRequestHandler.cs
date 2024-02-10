using Aplication.OverboardChess.Abstractions;
using Aplication.OverboardChess.Abstractions.Repositories.InvitationRepositories;
using Aplication.OverboardChess.Abstractions.Repositories.MeetingRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Requests.UpdateMeetingRequests
{
    public class JoinMeetingRequestHandler(
        IMeetingRepository meetingRepository, 
        IInvitationRepository invitationRepository,
        ICurrentIdentity currentIdentity) : IRequestHandler<JoinMeetingRequest>
    {
        public async Task Handle(JoinMeetingRequest request, CancellationToken cancellationToken)
        {
            var currentUserId = currentIdentity.GetUserId();
            var meeting = await meetingRepository.GetAsync(m => m.Id == request.MeetingId);
            meeting.Join(currentUserId);
            await meetingRepository.UpdateAsync(meeting);

            var invitation = await invitationRepository.GetAsync(i => i.MeetingId == request.MeetingId 
                && i.InvitedUserId == currentUserId
                && i.State == Domain.OverboardChess.Invitations.InvitationState.Created);

            if (invitation == null)
                return;

            invitation.Accept(currentUserId);
            await invitationRepository.UpdateAsync(invitation);
        }
    }
}
