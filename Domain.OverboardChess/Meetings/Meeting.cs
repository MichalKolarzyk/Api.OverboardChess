using Domain.OverboardChess.Base;

namespace Domain.OverboardChess.Meetings
{
    public class Meeting : AggregateRoot
    {
        public Meeting() { }

        public Meeting(string title)
        {
            Title = title;
        }


        public string Title { get; set; } = "";
    }
}
