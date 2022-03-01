namespace AuctionHouse.Application
{
    public interface IDataQueryable
    {
        Task<TResult> ExecuteQueryAsync<TResult>(
            string command,
            IDictionary<string, object> @params,
            Func<IEnumerable<dynamic>, TResult> map);
    }
}
