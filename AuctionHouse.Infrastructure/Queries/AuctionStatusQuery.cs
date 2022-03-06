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
    public class AuctionStatusQuery : PostgreSqlQuery<AuctionStatusQueryResponse>, IDataQueryable<AuctionStatusQueryResponse>
    {
        public AuctionStatusQuery(IDbConnection connection)
            : base(connection)
        {
        }

        protected override string Command => "SELECT id, current_price, bidder_member_id as winning_bidder_id, auction_ends FROM auction WHERE id = @AuctionId";
    }
}
