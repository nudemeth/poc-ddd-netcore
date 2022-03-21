namespace AuctionHouse.Domain
{
    public abstract record DomainEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();
    }
}
