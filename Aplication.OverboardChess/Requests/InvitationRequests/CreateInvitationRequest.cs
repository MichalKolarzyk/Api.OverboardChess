using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Requests.InvitationRequests
{
    public class CreateInvitationRequest : IRequest
    {
        public string Username { get; set; } = string.Empty;
        public Guid MeetingId { get; set; }
    }
}
