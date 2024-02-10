using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.OverboardChess.DomainBase
{
    internal interface IEntity
    {
        public Guid Id { get; }
    }
}
