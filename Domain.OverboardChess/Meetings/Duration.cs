using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OverboardChess.Meetings
{
    public readonly struct Duration
    {
        public int Hours { get; }
        public int Minutes { get; }

        public Duration(int hours)
        {
            Hours = hours;
            Minutes = 0;
        }

        public Duration(int hours, int minutes)
        {
            Hours = hours;
            Minutes = minutes;
        }

        public DateTime ToDateTime(DateTime from)
        {
            return from.AddHours(Hours)
                .AddMinutes(Minutes);
        }
    }
}
