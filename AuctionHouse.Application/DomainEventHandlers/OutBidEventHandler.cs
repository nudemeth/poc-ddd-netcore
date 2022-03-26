using AuctionHouse.Domain;
using AuctionHouse.Domain.Auction;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.DomainEventHandlers
{
    public class OutBidEventHandler : IConsumer<OutBidEvent>
    {
        public async Task Consume(ConsumeContext<OutBidEvent> context)
        {
            // Email customer to say that he has been out bid
            await context.RespondAsync(NoReplyMessage.Instance);
        }
    }
}
