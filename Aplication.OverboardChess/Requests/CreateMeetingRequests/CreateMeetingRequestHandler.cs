using Application.OverboardChess.Repositories;
using Domain.OverboardChess.Meetings;
using Domain.OverboardChess.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OverboardChess.Requests.CreateMeetingRequests
{
    public class CreateMeetingRequestHandler(IRepository<Meeting> meetingRepository, IRepository<User> userRepository) : IRequestHandler<CreateMeetingRequest>
    {
        private readonly IRepository<Meeting> _meetingRepository = meetingRepository;
        private readonly IRepository<User> _userRepository = userRepository;

        public async Task Handle(CreateMeetingRequest request, CancellationToken cancellationToken)
        {
            var duration = new Duration(request.DurationHours, request.DurationMinutes);

            var meeting = new Meeting(new User("", ""), request.Start, duration, request.Title);

            await _meetingRepository.InsertAsync(meeting);
        }
    }
}
