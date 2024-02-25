using Aplication.OverboardChess.Abstractions;
using Aplication.OverboardChess.Abstractions.Repositories.InvitationRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Requests.InvitationRequests
{
    public class RejectInvitationRequestHandler(IInvitationRepository invitationRepository, ICurrentIdentity currentIdentity) : IRequestHandler<RejectInvitationRequest>
    {
        public async Task Handle(RejectInvitationRequest request, CancellationToken cancellationToken)
        {
            var invitation = await invitationRepository.GetAsync(i => i.Id == request.InvitationId);
            invitation.Reject(currentIdentity.GetUserId());
            await invitationRepository.UpdateAsync(invitation);
        }
    }
}
