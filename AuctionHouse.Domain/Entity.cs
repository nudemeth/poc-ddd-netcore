using System.Collections.ObjectModel;

namespace AuctionHouse.Domain
{
    public class Entity
    {
        private IList<DomainEvent> domainEvents;

        protected Entity()
        {
            domainEvents = new List<DomainEvent>();
        }
        
        public Guid Id { get; protected set; }
        public IReadOnlyCollection<DomainEvent> DomainEvents => new ReadOnlyCollection<DomainEvent>(domainEvents);

        public void AddDomainEvent(DomainEvent domainEvent)
        {
            this.domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            this.domainEvents.Clear();
        }

        public void RemoveDomainEvent(DomainEvent domainEvent)
        {
            if (domainEvents.Contains(domainEvent))
            {
                this.domainEvents.Remove(domainEvent);
            }
        }
    }
}
