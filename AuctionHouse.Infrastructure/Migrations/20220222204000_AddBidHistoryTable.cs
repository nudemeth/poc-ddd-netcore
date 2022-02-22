using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Infrastructure.Migrations
{
    [Migration("20220222204000")]
    public class AddBidHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bid_history",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    auction_id = table.Column<Guid>(nullable: false),
                    bidder_id = table.Column<Guid>(nullable: false),
                    bid = table.Column<int>(nullable: false),
                    time_of_bid = table.Column<DateTime>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("bid_history_pk", t => t.id);
                });
        }
    }
}
