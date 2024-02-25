using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OverboardChess.Requests.MeetingRequests
{
    public class CreateMeetingRequest(string title, DateTime start, int durationHours, int durationMinutes) : IRequest
    {
        public string Title { get; } = title;
        public DateTime Start { get; set; } = start;
        public int DurationHours { get; set; } = durationHours;
        public int DurationMinutes { get; set; } = durationMinutes;
    }
}
