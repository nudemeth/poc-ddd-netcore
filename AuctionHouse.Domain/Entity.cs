namespace AuctionHouse.Domain
{
    public class Entity<TId>
        where TId : struct
    {
        public TId Id { get; protected set; }
        public long Version { get; private set; }
    }
}
