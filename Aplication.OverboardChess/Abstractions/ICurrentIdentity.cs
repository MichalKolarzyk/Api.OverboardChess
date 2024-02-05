using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Abstractions
{
    public interface ICurrentIdentity
    {
        public Guid? UserId { get; }
    }
}
