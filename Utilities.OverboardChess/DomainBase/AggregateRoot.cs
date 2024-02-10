using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.OverboardChess.DomainBase
{
    public class AggregateRoot : IEntity
    {
        public Guid Id { get; set; }

        private readonly List<IDomainEvent> _events = [];
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _events.Add(domainEvent);
        }

        public List<IDomainEvent> GetDomainEvents() 
        { 
            return _events; 
        }

        public void RemoveDomainEvents()
        {
            _events.Clear();
        }
    }
}
