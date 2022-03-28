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
    public class OutBidEventHandler : DomainEventHandler<OutBidEvent>
    {
        protected override Task Handle(OutBidEvent @event)
        {
            // Email customer to say that he has been out bid
            return Task.CompletedTask;
        }
    }
}
