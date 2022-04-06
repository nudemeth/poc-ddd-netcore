using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AuctionHouse.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "auction",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    starting_price = table.Column<decimal>(type: "numeric", nullable: false),
                    auction_ends = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auction", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "bid_history",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    auction_id = table.Column<Guid>(type: "uuid", nullable: false),
                    bidder_id = table.Column<Guid>(type: "uuid", nullable: false),
                    bid = table.Column<decimal>(type: "numeric", nullable: false),
                    time_of_bid = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bid_history", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "winning_bid",
                columns: table => new
                {
                    auction_id = table.Column<Guid>(type: "uuid", nullable: false),
                    bidder_member_id = table.Column<Guid>(type: "uuid", nullable: false),
                    maximum_bid = table.Column<decimal>(type: "numeric", nullable: false),
                    time_of_bid = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    current_price = table.Column<decimal>(type: "numeric", nullable: false),
                    version = table.Column<long>(type: "bigint", rowVersion: true, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_winning_bid", x => x.auction_id);
                    table.ForeignKey(
                        name: "FK_winning_bid_auction_auction_id",
                        column: x => x.auction_id,
                        principalTable: "auction",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bid_history");

            migrationBuilder.DropTable(
                name: "winning_bid");

            migrationBuilder.DropTable(
                name: "auction");
        }
    }
}
