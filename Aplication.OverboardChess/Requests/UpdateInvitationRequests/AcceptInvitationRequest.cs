using MediatR;

namespace Aplication.OverboardChess.Requests.UpdateInvitationRequests
{
    public class AcceptInvitationRequest : IRequest
    {
        public Guid InvitationId { get; set; }
    }
}
