namespace AuctionHouse.Application
{
    public interface IUnitOfWork
    {
        Task SaveAsync();

        Task ClearAsync();
    }
}
