using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionHouse.Application;
using Dapper;

namespace AuctionHouse.Infrastructure.Queries
{
    public abstract class PostgreSqlQuery<TResult> : IDataQueryable<TResult>
    {
        private readonly IDbConnection connection;

        public PostgreSqlQuery(IDbConnection connection)
        {
            this.connection = connection;
        }

        protected abstract string Command { get; }

        public virtual async Task<TResult> ExecuteQueryAsync<TData>(IDictionary<string, object> @params, Func<IEnumerable<TData>, TResult> map)
        {
            var result = await connection.QueryAsync<TData>(Command, @params);
            return map(result);
        }
    }
}
