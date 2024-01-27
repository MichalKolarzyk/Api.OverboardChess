using Domain.OverboardChess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Meetings
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
