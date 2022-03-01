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
    public class PostgreSqlQuery : IDataQueryable
    {
        private readonly IDbConnection connection;

        public PostgreSqlQuery(IDbConnection connection)
        {
            this.connection = connection;
        }

        public async Task<TResult> ExecuteQueryAsync<TResult>(string command, IDictionary<string, object> @params, Func<IEnumerable<dynamic>, TResult> map)
        {
            var result = await connection.QueryAsync(command, @params);
            return map(result);
        }
    }
}
