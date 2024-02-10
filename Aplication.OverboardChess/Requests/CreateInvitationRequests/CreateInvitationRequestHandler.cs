using Aplication.OverboardChess.Abstractions;
using Aplication.OverboardChess.Abstractions.Repositories;
using Aplication.OverboardChess.Abstractions.Repositories.MeetingRepositories;
using Domain.OverboardChess.Invitations;
using MediatR;

namespace Aplication.OverboardChess.Requests.CreateInvitationRequests
{
    public class CreateInvitationRequestHandler(IRepository<Invitation> invitationRepository, IMeetingRepository meetingRepository, ICurrentIdentity currentIdentity) : IRequestHandler<CreateInvitationRequest>
    {
        public async Task Handle(CreateInvitationRequest request, CancellationToken cancellationToken)
        {
            var meeting = await meetingRepository.GetAsync(request.MeetingId);

            if (meeting.IsParticipant(currentIdentity.GetUserId()))
                throw new Exception("User is already meeting participant");

            var invitationExists = await invitationRepository.Any(i => i.InvitedUserId == request.UserId 
                && i.MeetingId == request.MeetingId 
                && i.State == InvitationState.Created);

            if (invitationExists)
                throw new Exception("User is already invited to this meeting.");

            var invitation = new Invitation(request.MeetingId, currentIdentity.GetUserId(), request.UserId);

            await invitationRepository.InsertAsync(invitation);
        }
    }
}
