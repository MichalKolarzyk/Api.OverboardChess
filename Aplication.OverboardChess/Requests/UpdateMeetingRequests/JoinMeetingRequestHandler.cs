using Aplication.OverboardChess.Abstractions;
using Aplication.OverboardChess.Abstractions.Repositories.MeetingRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Requests.UpdateMeetingRequests
{
    public class JoinMeetingRequestHandler(IMeetingRepository meetingRepository, ICurrentIdentity currentIdentity) : IRequestHandler<JoinMeetingRequest>
    {
        public async Task Handle(JoinMeetingRequest request, CancellationToken cancellationToken)
        {
            var meeting = await meetingRepository.GetAsync(m => m.Id == request.MeetingId);
            meeting.Join(currentIdentity.GetUserId());
            await meetingRepository.UpdateAsync(meeting);
        }
    }
}
