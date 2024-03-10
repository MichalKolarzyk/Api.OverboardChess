using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Providers
{
    public interface IDateTimeProvider
    {
        public DateTime Now { get; }
    }
}
