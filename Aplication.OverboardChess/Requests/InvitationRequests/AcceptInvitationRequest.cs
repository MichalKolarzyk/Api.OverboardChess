using MediatR;

namespace Aplication.OverboardChess.Requests.InvitationRequests
{
    public class AcceptInvitationRequest : IRequest
    {
        public Guid InvitationId { get; set; }
    }
}
