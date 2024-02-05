using Aplication.OverboardChess.Abstractions;
using Domain.OverboardChess.Invitations;
using Domain.OverboardChess.Users;
using MediatR;

namespace Aplication.OverboardChess.Requests.UpdateInvitationRequests
{
    public class AcceptInvitationRequestHandler(IRepository<Invitation> invitationRepository, ICurrentIdentity currentIdentity, IRepository<User> userRepository) : IRequestHandler<AcceptInvitationRequest>
    {
        private readonly IRepository<Invitation> _invitationRepository = invitationRepository;
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly ICurrentIdentity _currentIdentity = currentIdentity;

        public async Task Handle(AcceptInvitationRequest request, CancellationToken cancellationToken)
        {
            var requestedBy = await _userRepository.GetAsync(_currentIdentity.UserId.Value);
            var invitation = await _invitationRepository.GetAsync(request.InvitationId);

            invitation.Accept(requestedBy);
        }
    }
}
