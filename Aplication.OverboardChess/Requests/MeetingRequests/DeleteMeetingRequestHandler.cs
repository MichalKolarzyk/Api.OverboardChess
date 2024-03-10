using Aplication.OverboardChess.Abstractions;
using Aplication.OverboardChess.Abstractions.Repositories;
using Domain.OverboardChess.Meetings;
using Domain.OverboardChess.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Requests.MeetingRequests
{
    public class DeleteMeetingRequestHandler(ICurrentIdentity currentIdentity, IRepository<User> userRepository, IRepository<Meeting> meetingRepository) : IRequestHandler<DeleteMeetingRequest>
    {
        public async Task Handle(DeleteMeetingRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetAsync(u => u.Id == currentIdentity.UserId);
            var meeting = await meetingRepository.GetAsync(request.MeetingId);

            if (meeting.OwnerId != user.Id)
                throw new Exception("Only user owner can delete meeting.");

            await meetingRepository.DeleteAsync(m => m.Id == request.MeetingId);
        }
    }
}
