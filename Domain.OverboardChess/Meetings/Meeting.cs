using Domain.OverboardChess.Base;
using Domain.OverboardChess.Users;

namespace Domain.OverboardChess.Meetings
{
    public class Meeting : AggregateRoot
    {
        public Guid OwnerId { get; set; }
        public string Title { get; set; } = "";
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public MeetingState State { get; set; }

        public Meeting() { }

        public Meeting(User owner, DateTime start, Duration duration, string title)
        {
            Title = title;
            OwnerId = owner.Id;
            Start = start;
            End = duration.ToDateTime(start);
            State = MeetingState.Ready;
        }
    }

    public enum MeetingState
    {
        Ready,
        InProgress,
        Done,
    }
}
