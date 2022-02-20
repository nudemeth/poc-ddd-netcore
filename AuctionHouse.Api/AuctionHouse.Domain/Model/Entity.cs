namespace AuctionHouse.Domain.Model
{
    public class Entity<TId>
        where TId : struct
    {
        public TId Id { get; protected set; }
        public int Version { get; private set; }
    }
}
