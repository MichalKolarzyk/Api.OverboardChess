using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Requests.InvitationRequests
{
    public class RejectInvitationRequest : IRequest
    {
        public Guid InvitationId { get; set; }
    }
}
