using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Domain.Auction
{
    public sealed record Money : ValueObject<Money>
    {
        private readonly decimal value;

        public Money()
            : this(0m)
        {
        }

        public Money(decimal value)
        {
            if (value % 0.01m != 0)
                throw new MoreThanTwoDecimalPlacesInMoneyValueException();

            if (value < 0)
                throw new MoneyCannotBeANegativeValueException();

            this.value = value;
        }

        internal Money Add(Money money)
        {
            return new Money(value + money.value);
        }

        internal bool IsGreaterThan(Money money)
        {
            return this.value > money.value;
        }

        internal bool IsGreaterThanOrEqualTo(Money money)
        {
            return this.value > money.value || this.Equals(money);
        }

        internal bool IsLessThanOrEqualTo(Money money)
        {
            return this.value < money.value || this.Equals(money);
        }

        public override string ToString()
        {
            return string.Format("{0}", value);
        }
    }
}
