using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Requests.MeetingRequests
{
    public class GetMeetingRequest(Guid meetingId) : IRequest<GetMeetingResponse>
    {
        public Guid MeetingId { get; } = meetingId;
    }
}
