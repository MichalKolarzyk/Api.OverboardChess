using Application.OverboardChess.Repositories;
using Domain.OverboardChess.Meetings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OverboardChess.Requests.CreateMeetingRequests
{
    public class CreateMeetingRequestHandler(IRepository<Meeting> meetingRepository) : IRequestHandler<CreateMeetingRequest>
    {
        private readonly IRepository<Meeting> _meetingRepository = meetingRepository;

        public async Task Handle(CreateMeetingRequest request, CancellationToken cancellationToken)
        {
            var meeting = new Meeting(request.Title);

            await _meetingRepository.InsertAsync(meeting);
        }
    }
}
