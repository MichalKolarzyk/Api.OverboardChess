using Aplication.OverboardChess.Abstractions;
using Aplication.OverboardChess.Abstractions.Repositories;
using Domain.OverboardChess.Invitations;
using Domain.OverboardChess.Meetings;
using Domain.OverboardChess.Users;
using MediatR;

namespace Aplication.OverboardChess.Requests.InvitationRequests
{
    public class AcceptInvitationRequestHandler(
        IRepository<Invitation> invitationRepository,
        ICurrentIdentity currentIdentity,
        IRepository<Meeting> meetingRepository,
        IRepository<User> userRepository) : IRequestHandler<AcceptInvitationRequest>
    {
        public async Task Handle(AcceptInvitationRequest request, CancellationToken cancellationToken)
        {
            var requestedBy = await userRepository.GetAsync(currentIdentity.GetUserId());
            var invitation = await invitationRepository.GetAsync(request.InvitationId);
            invitation.Accept(requestedBy.Id);
            await invitationRepository.UpdateAsync(invitation);

            var meeting = await meetingRepository.GetAsync(invitation.MeetingId);
            meeting.AddParticipant(invitation.InvitedUserId);
            await meetingRepository.UpdateAsync(meeting);
        }
    }
}
