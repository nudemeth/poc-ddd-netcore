using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionHouse.Application;
using AuctionHouse.Application.Queries;
using Dapper;

namespace AuctionHouse.Infrastructure.Queries
{
    public abstract class SqlQuery<TResult> : IDataQueryable<TResult>
    {
        private readonly IDbConnection connection;

        public SqlQuery(IDbConnection connection)
        {
            this.connection = connection;
        }

        protected abstract string Command { get; }

        public async Task<TResult> ExecuteQueryAsync<TData>(IDictionary<string, object> @params, Func<IEnumerable<TData>, TResult> map)
        {
            var result = await connection.QueryAsync<TData>(Command, @params);
            return map(result);
        }
    }
}
