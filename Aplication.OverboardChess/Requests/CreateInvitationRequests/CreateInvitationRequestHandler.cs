using Aplication.OverboardChess.Abstractions;
using Aplication.OverboardChess.Abstractions.Repositories;
using Domain.OverboardChess.Invitations;
using Domain.OverboardChess.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Requests.CreateInvitationRequests
{
    public class CreateInvitationRequestHandler(IRepository<Invitation> invitationRepository, ICurrentIdentity currentIdentity) : IRequestHandler<CreateInvitationRequest>
    {
        private readonly IRepository<Invitation> _invitationRepository = invitationRepository;
        private readonly ICurrentIdentity _currentIdentity = currentIdentity;

        public async Task Handle(CreateInvitationRequest request, CancellationToken cancellationToken)
        {
            var invitationExists = await _invitationRepository.Any(i => i.InvitedUserId == request.UserId 
                && i.MeetingId == request.MeetingId 
                && i.State == InvitationState.Created);

            if (invitationExists)
                throw new Exception("User is already invited to this meeting.");

            var invitation = new Invitation(request.MeetingId, _currentIdentity.GetUserId(), request.UserId);

            await _invitationRepository.InsertAsync(invitation);
        }
    }
}
