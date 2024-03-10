using Aplication.OverboardChess.Abstractions;
using Aplication.OverboardChess.Abstractions.Repositories;
using Aplication.OverboardChess.Abstractions.Repositories.MeetingRepositories;
using Domain.OverboardChess.Invitations;
using Domain.OverboardChess.Users;
using MediatR;

namespace Aplication.OverboardChess.Requests.InvitationRequests
{
    public class CreateInvitationRequestHandler(IRepository<Invitation> invitationRepository, IRepository<User> userRepository, IMeetingRepository meetingRepository, ICurrentIdentity currentIdentity) : IRequestHandler<CreateInvitationRequest>
    {
        public async Task Handle(CreateInvitationRequest request, CancellationToken cancellationToken)
        {
            var meeting = await meetingRepository.GetAsync(request.MeetingId);

            var invitedUser = await userRepository.GetAsync(u => u.Username == request.Username)
                ?? throw new Exception($"User with name {request.Username} does not exists.");

            if (meeting.IsParticipant(invitedUser.Id))
                throw new Exception("User is already meeting participant");

            var invitationExists = await invitationRepository.Any(i => i.InvitedUserId == invitedUser.Id 
                && i.MeetingId == request.MeetingId 
                && i.State == InvitationState.Created);

            if (invitationExists)
                throw new Exception("User is already invited to this meeting.");

            var invitation = new Invitation(request.MeetingId, currentIdentity.GetUserId(), invitedUser.Id);

            await invitationRepository.InsertAsync(invitation);
        }
    }
}
