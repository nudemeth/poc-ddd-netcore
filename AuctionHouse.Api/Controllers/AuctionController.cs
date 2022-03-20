using AuctionHouse.Application.Commands;
using AuctionHouse.Application.Queries;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace AuctionHouse.Api.Controllers
{
    [ApiController]
    [Route("auctions")]
    public class AuctionController : ControllerBase
    {
        private readonly ILogger<AuctionController> logger;
        private readonly IRequestClient<BidOnAuctionCommandRequest> bidOnAuctionClient;
        private readonly IRequestClient<CreateAuctionCommandRequest> createAuctionClient;
        private readonly IRequestClient<AuctionStatusQueryRequest> auctionStatusClient;
        private readonly IRequestClient<BidHistoryQueryRequest> bidHistoryClient;

        public AuctionController(
            IRequestClient<BidOnAuctionCommandRequest> bidOnAuctionClient,
            IRequestClient<CreateAuctionCommandRequest> createAuctionClient,
            IRequestClient<AuctionStatusQueryRequest> auctionStatusClient,
            IRequestClient<BidHistoryQueryRequest> bidHistoryClient,
            ILogger<AuctionController> logger)
        {
            this.bidOnAuctionClient = bidOnAuctionClient;
            this.createAuctionClient = createAuctionClient;
            this.auctionStatusClient = auctionStatusClient;
            this.bidHistoryClient = bidHistoryClient;
            this.logger = logger;
        }

        [HttpGet("{auctionId}")]
        public async Task<IActionResult> Get([FromRoute] Guid auctionId)
        {
            var request = new AuctionStatusQueryRequest { AuctionId = auctionId };
            var response = await auctionStatusClient.GetResponse<AuctionStatusQueryResponse>(request);
            return this.Ok(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuctionCommandRequest request)
        {
            var response = await createAuctionClient.GetResponse<CreateAuctionCommandResponse>(request);
            return this.Ok(response.Message);
        }

        [HttpGet("{auctionId}/bids")]
        public async Task<IActionResult> GetBidHistory([FromRoute] Guid auctionId)
        {
            var request = new BidHistoryQueryRequest { AuctionId = auctionId };
            var response = await bidHistoryClient.GetResponse<BidHistoryQueryResponse>(request);
            return this.Ok(response.Message);
        }

        [HttpPost("{auctionId}/bids")]
        public async Task<IActionResult> Bid([FromRoute] Guid auctionId, [FromBody] BidOnAuctionRequest body)
        {
            var request = new BidOnAuctionCommandRequest { AuctionId = auctionId, MemberId = body.MemberId, Amount = body.Amount };
            var response = await bidOnAuctionClient.GetResponse<BidOnAuctionCommandResponse>(request);
            return this.Ok(response.Message);
        }

        public class BidOnAuctionRequest
        {
            public Guid MemberId { get; init; }
            public decimal Amount { get; init; }
        }
    }
}