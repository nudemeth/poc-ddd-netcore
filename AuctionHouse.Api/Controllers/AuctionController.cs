using AuctionHouse.Application.Commands;
using AuctionHouse.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuctionHouse.Api.Controllers
{
    [ApiController]
    [Route("auctions")]
    public class AuctionController : ControllerBase
    {
        private readonly ILogger<AuctionController> logger;
        private readonly IMediator mediator;

        public AuctionController(
            IMediator mediator,
            ILogger<AuctionController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpGet("{auctionId}")]
        public async Task<IActionResult> Get([FromRoute] Guid auctionId)
        {
            var request = new AuctionStatusQueryRequest { AuctionId = auctionId };
            var response = await mediator.Send(request);
            return this.Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuctionCommandRequest request)
        {
            var response = await mediator.Send(request);
            return this.Ok(response);
        }

        [HttpGet("{auctionId}/bids")]
        public async Task<IActionResult> GetBidHistory([FromRoute] Guid auctionId)
        {
            var request = new BidHistoryQueryRequest { AuctionId = auctionId };
            var response = await mediator.Send(request);
            return this.Ok(response);
        }

        [HttpPost("{auctionId}/bids")]
        public async Task<IActionResult> Bid([FromRoute] Guid auctionId, [FromBody] BidOnAuctionRequest body)
        {
            var request = new BidOnAuctionCommandRequest { AuctionId = auctionId, MemberId = body.MemberId, Amount = body.Amount };
            var response = await mediator.Send(request);
            return this.Ok(response);
        }

        public class BidOnAuctionRequest
        {
            public Guid MemberId { get; init; }
            public decimal Amount { get; init; }
        }
    }
}