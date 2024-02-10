using Aplication.OverboardChess.Abstractions;
using Aplication.OverboardChess.Abstractions.Repositories;
using Aplication.OverboardChess.Abstractions.Repositories.InvitationRepositories;
using Aplication.OverboardChess.Abstractions.Repositories.InvitationRepositories.ViewModels;
using Domain.OverboardChess.Invitations;
using MediatR;
namespace Aplication.OverboardChess.Requests.GetInvitationRequests
{
    public class GetRecivedInvitationsRequestHandler(ICurrentIdentity currentIdentity, IInvitationRepository invitationRepository) : IRequestHandler<GetRecivedInvitationsRequest, List<RecivedInvitationViewModel>>
    {
        public async Task<List<RecivedInvitationViewModel>> Handle(GetRecivedInvitationsRequest request, CancellationToken cancellationToken)
        {
            var invitedUser = currentIdentity.GetUserId();
            return await invitationRepository.GetRecivedInvitiationsViewModel(invitedUser);
        }
    }
}
