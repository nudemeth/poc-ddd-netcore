namespace AuctionHouse.Application
{
    public interface IDataQueryable<TResult>
    {
        Task<TResult> ExecuteQueryAsync(
            IDictionary<string, object> @params,
            Func<IEnumerable<dynamic>, TResult> map);
    }
}
