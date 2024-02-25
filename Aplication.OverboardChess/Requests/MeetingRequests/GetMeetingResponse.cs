using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Requests.MeetingRequests
{
    public class GetMeetingResponse
    {
        public Guid Id { get; set; }
        public bool CanJoin { get; set; }
        public bool CanEdit { get; set; }
        public bool CanRemove { get; set; }
        public bool IsOwner { get; set; }
        public bool IsParticipant { get; set; }
        public int DurationHours { get; set; }
        public int DurationMinutes { get; set; }
        public DateTime Start {  get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
