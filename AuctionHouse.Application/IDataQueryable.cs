namespace AuctionHouse.Application
{
    public interface IDataQueryable
    {
        Task<TResult> GetAuctionStatusAsync<TResult>(Func<IEnumerable<dynamic>, TResult> map, IDictionary<string, object> @params);

        Task<TResult> GetBidHistoryAsync<TResult>(Func<IEnumerable<dynamic>, TResult> map, IDictionary<string, object> @params);
    }
}
