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
    public class GetMeetingRequestHandler(ICurrentIdentity currentIdentity, IRepository<User> userRepository, IRepository<Meeting> meetingRepository) : IRequestHandler<GetMeetingRequest, GetMeetingResponse>
    {
        public async Task<GetMeetingResponse> Handle(GetMeetingRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetAsync(currentIdentity.GetUserId());
            var meeting = await meetingRepository.GetAsync(request.MeetingId);

            var duration = meeting.GetDuration();
            return new GetMeetingResponse
            {
                CanJoin = meeting.CanJoin(user.Id),
                CanEdit = meeting.IsOwner(user.Id),
                CanRemove = meeting.IsOwner(user.Id),
                IsOwner = meeting.IsOwner(user.Id),
                IsParticipant = meeting.IsParticipant(user.Id),
                Id = meeting.Id,
                DurationHours = duration.Hours,
                DurationMinutes = duration.Minutes,
                Start = meeting.Start,
                Title = meeting.Title,
                Description = "This will be add in future",
            };
        }
    }
}
