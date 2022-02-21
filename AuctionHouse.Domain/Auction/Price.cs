using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Domain.Auction
{
    internal record Price : ValueObject<Price>
    {
        internal Price(Money amount)
        {
            if (amount == null)
                throw new ArgumentNullException("Amount cannot be null");

            Amount = amount;
        }

        internal Money Amount { get; private set; }

        internal Money BidIncrement()
        {
            if (Amount.IsGreaterThanOrEqualTo(new Money(0.01m)) && Amount.IsLessThanOrEqualTo(new Money(0.99m)))
                return Amount.Add(new Money(0.05m));

            if (Amount.IsGreaterThanOrEqualTo(new Money(1.00m)) && Amount.IsLessThanOrEqualTo(new Money(4.99m)))
                return Amount.Add(new Money(0.20m));

            if (Amount.IsGreaterThanOrEqualTo(new Money(5.00m)) && Amount.IsLessThanOrEqualTo(new Money(14.99m)))
                return Amount.Add(new Money(0.50m));

            return Amount.Add(new Money(1.00m));

        }

        internal bool CanBeExceededBy(Money offer)
        {
            return offer.IsGreaterThanOrEqualTo(BidIncrement());
        }
    }
}
