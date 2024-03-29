﻿namespace AuctionHouse.Application.Queries
{
    public interface IDataQueryable<TResult>
    {
        Task<TResult> ExecuteQueryAsync<TData>(
            IDictionary<string, object> @params,
            Func<IEnumerable<TData>, TResult> map);
    }
}
