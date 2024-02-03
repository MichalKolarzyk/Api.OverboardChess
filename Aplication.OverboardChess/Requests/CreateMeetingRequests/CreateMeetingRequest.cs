using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OverboardChess.Requests.CreateMeetingRequests
{
    public class CreateMeetingRequest(string title) : IRequest
    {
        public string Title { get; } = title;
    }
}
