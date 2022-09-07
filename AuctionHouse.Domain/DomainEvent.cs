using MediatR;

namespace AuctionHouse.Domain
{
    public abstract record DomainEvent : INotification
    {
        public Guid EventId { get; } = Guid.NewGuid();
    }
}
