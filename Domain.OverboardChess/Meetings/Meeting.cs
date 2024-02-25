using Domain.OverboardChess.Users;
using Utilities.OverboardChess.DomainBase;

namespace Domain.OverboardChess.Meetings
{
    public class Meeting : AggregateRoot
    {
        public Guid OwnerId { get; set; }
        public string Title { get; set; } = "";
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public MeetingState State { get; set; }
        public List<Guid> Participants { get; set; } = [];
        public int? ParticipantsLimit { get; set; }
        public bool IsPrivate = false;

        public Meeting() { }

        public static Meeting Create(User owner, DateTime start, Duration duration, string title, int? participantsLimit = null, bool isPrivate = false)
        {
            if (duration.InMinutes() == 0)
                throw new Exception("Meeting have to have duration greater then 0 minutes");

            return new Meeting
            {
                Title = title,
                OwnerId = owner.Id,
                Start = start,
                End = duration.ToDateTime(start),
                State = MeetingState.Ready,
                ParticipantsLimit = participantsLimit,
                IsPrivate = isPrivate,
            };
        }

        public void Join(Guid userId)
        {
            if (IsParticipant(userId))
                throw new Exception("User is already meeting participant");

            if(IsPrivate)
                throw new Exception("User cannot join. The meeting is private. You can only join by invitation.");

            if (Participants.Count >= ParticipantsLimit)
                throw new Exception("Participants limit has been reached");

            Participants.Add(userId);
        }

        public bool CanJoin(Guid userId)
        {
            return !IsParticipant(userId)
                && Participants.Count < ParticipantsLimit
                && !IsPrivate;
        }

        public bool IsOwner(Guid userId)
        {
            return OwnerId == userId;
        }

        public void AddParticipant(Guid userId)
        {
            if (IsParticipant(userId))
                throw new Exception("User is already meeting participant");

            if (Participants.Count >= ParticipantsLimit)
                throw new Exception("Participants limit has been reached");

            Participants.Add(userId);
        }

        public bool IsParticipant(Guid userId)
        {
            return Participants.Contains(userId) || OwnerId == userId;
        }

        public TimeSpan GetDuration()
        {
            return End - Start;
        }
    }

    public enum MeetingState
    {
        Ready,
        InProgress,
        Done,
    }
}
