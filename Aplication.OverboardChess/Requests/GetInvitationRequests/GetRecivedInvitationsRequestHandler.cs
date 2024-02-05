using Aplication.OverboardChess.Abstractions;
using Domain.OverboardChess.Invitations;
using MediatR;
namespace Aplication.OverboardChess.Requests.GetInvitationRequests
{
    public class GetRecivedInvitationsRequestHandler(ICurrentIdentity currentIdentity, IRepository<Invitation> invitationRepository) : IRequestHandler<GetRecivedInvitationsRequest, GetRecivedInvitationsResponse>
    {
        private readonly ICurrentIdentity _currentIdentity = currentIdentity;
        private readonly IRepository<Invitation> _invitationRepository = invitationRepository;

        public async Task<GetRecivedInvitationsResponse> Handle(GetRecivedInvitationsRequest request, CancellationToken cancellationToken)
        {
            var invitedUser = _currentIdentity.GetUserId();

            var invitations = await _invitationRepository.GetListAsync(i => i.InvitedUserId == invitedUser && i.State == InvitationState.Created);

            return new GetRecivedInvitationsResponse
            {
                RecivedInvitations = invitations.Select(i => i.Id).ToList(),
            };
        }
    }
}
