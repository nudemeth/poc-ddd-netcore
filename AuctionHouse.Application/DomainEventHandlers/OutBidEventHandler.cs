using AuctionHouse.Domain;
using AuctionHouse.Domain.Auction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.DomainEventHandlers
{
    public class OutBidEventHandler : INotificationHandler<OutBidEvent>
    {
        public async Task Handle(OutBidEvent notification, CancellationToken cancellationToken)
        {
            // Email customer to say that he has been out bid
        }
    }
}
