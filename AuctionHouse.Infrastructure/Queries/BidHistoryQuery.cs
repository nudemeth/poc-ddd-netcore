using AuctionHouse.Application;
using AuctionHouse.Application.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Infrastructure.Queries
{
    public class BidHistoryQuery : PostgreSqlQuery<BidHistoryQueryResponse>, IDataQueryable<BidHistoryQueryResponse>
    {
        public BidHistoryQuery(IDbConnection connection)
            : base(connection)
        {
        }

        protected override string Command => "SELECT bidder_id, bid, time_of_bid FROM bid_history WHERE auction_id = @AuctionId ORDER BY bid DESC, time_of_bid ASC";
    }
}
