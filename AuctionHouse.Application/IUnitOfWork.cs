using AuctionHouse.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application
{
    public interface IUnitOfWork
    {
        Task SaveAsync();

        Task<TResult> ExecuteRawQueryAsync<TResult>(string rawQuery, Func<IEnumerable<dynamic>, TResult> map, params object[] @params);

        Task ClearAsync();
    }
}
