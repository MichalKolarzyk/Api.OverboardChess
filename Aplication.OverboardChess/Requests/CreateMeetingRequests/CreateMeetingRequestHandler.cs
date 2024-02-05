using Aplication.OverboardChess.Abstractions;
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
    public class CreateMeetingRequestHandler(IRepository<Meeting> meetingRepository, IRepository<User> userRepository, ICurrentIdentity currentIdentity) : IRequestHandler<CreateMeetingRequest>
    {
        private readonly IRepository<Meeting> _meetingRepository = meetingRepository;
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly ICurrentIdentity _currentIdentity = currentIdentity;

        public async Task Handle(CreateMeetingRequest request, CancellationToken cancellationToken)
        {
            if (_currentIdentity.UserId == null)
                throw new Exception("");

            var user = await _userRepository.GetAsync(_currentIdentity.UserId.Value);
            var duration = new Duration(request.DurationHours, request.DurationMinutes);
            var meeting = new Meeting(user, request.Start, duration, request.Title);

            await _meetingRepository.InsertAsync(meeting);
        }
    }
}
