using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OverboardChess.Base
{
    internal interface IEntity
    {
        public Guid Id { get; }
    }
}
