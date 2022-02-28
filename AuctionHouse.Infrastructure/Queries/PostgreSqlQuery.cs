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

        public async Task<TResult> GetAuctionStatusAsync<TResult>(Func<IEnumerable<dynamic>, TResult> map, IDictionary<string, object> @params)
        {
            var sql = "SELECT bidder_id, bid, time_of_bid FROM bid_history WHERE auction_id = @AuctionId ORDER BY bid DESC, time_of_bid ASC";
            var result = await connection.QueryAsync(sql, @params);
            return map(result);
        }

        public async Task<TResult> GetBidHistoryAsync<TResult>(Func<IEnumerable<dynamic>, TResult> map, IDictionary<string, object> @params)
        {
            var sql = "SELECT id, current_price, bidder_member_id as winning_bidder_id, auction_ends FROM auction WHERE id = @AuctionId";
            var result = await connection.QueryAsync(sql, @params);
            return map(result);
        }
    }
}
