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
    public class AuctionStatusQuery : SqlQuery<AuctionStatusQueryResponse>, IDataQueryable<AuctionStatusQueryResponse>
    {
        public AuctionStatusQuery(IDbConnection connection)
            : base(connection)
        {
        }

        protected override string Command =>
            "SELECT a.id as Id, wb.current_price as CurrentPrice, wb.bidder_member_id as WinningBidderId, a.auction_ends as AuctionEnds " +
            "FROM auction a " +
            "LEFT OUTER JOIN winning_bid wb ON a.id = wb.auction_id " +
            "WHERE a.id = @AuctionId";
    }
}
