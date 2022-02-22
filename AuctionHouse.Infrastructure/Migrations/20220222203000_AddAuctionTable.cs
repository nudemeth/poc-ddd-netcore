using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Infrastructure.Migrations
{
    [Migration("20220222203000")]
    public class AddAuctionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "auction",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    auction_ends = table.Column<DateTime>(nullable: false),
                    bidder_memeber_id = table.Column<string>(),
                    current_price = table.Column<decimal>(),
                    maximum_bid = table.Column<decimal>(),
                    next_bid_increment = table.Column<decimal>(),
                    starting_price = table.Column<decimal>(nullable: false),
                    time_of_bid = table.Column<DateTime>(),
                    version = table.Column<int>(nullable: false, rowVersion: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("auction_pk", t => t.id);
                });
        }
    }
}
