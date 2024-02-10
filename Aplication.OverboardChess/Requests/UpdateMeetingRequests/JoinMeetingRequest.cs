using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Requests.UpdateMeetingRequests
{
    public class JoinMeetingRequest : IRequest
    {
        public Guid MeetingId { get; set; }

        public JoinMeetingRequest(Guid meetingId)
        {
            MeetingId = meetingId;
        }
    }
}
