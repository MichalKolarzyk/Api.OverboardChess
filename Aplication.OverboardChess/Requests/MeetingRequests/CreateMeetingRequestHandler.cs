using Aplication.OverboardChess.Abstractions;
using Aplication.OverboardChess.Abstractions.Repositories;
using Domain.OverboardChess.Meetings;
using Domain.OverboardChess.Users;
using MediatR;

namespace Application.OverboardChess.Requests.MeetingRequests
{
    public class CreateMeetingRequestHandler(IRepository<Meeting> meetingRepository, IRepository<User> userRepository, ICurrentIdentity currentIdentity) : IRequestHandler<CreateMeetingRequest>
    {
        private readonly IRepository<Meeting> _meetingRepository = meetingRepository;
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly ICurrentIdentity _currentIdentity = currentIdentity;

        public async Task Handle(CreateMeetingRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(_currentIdentity.GetUserId());
            var duration = new Duration(request.DurationHours, request.DurationMinutes);
            var meeting = Meeting.Create(user, request.Start, duration, request.Title);

            await _meetingRepository.InsertAsync(meeting);
        }
    }
}
