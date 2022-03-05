using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Domain.Auction
{
    public sealed record Money : ValueObject<Money>
    {
        private decimal Value { get; }

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

            this.Value = value;
        }

        internal Money Add(Money money)
        {
            return new Money(Value + money.Value);
        }

        internal bool IsGreaterThan(Money money)
        {
            return this.Value > money.Value;
        }

        internal bool IsGreaterThanOrEqualTo(Money money)
        {
            return this.Value > money.Value || this.Equals(money);
        }

        internal bool IsLessThanOrEqualTo(Money money)
        {
            return this.Value < money.Value || this.Equals(money);
        }

        public override string ToString()
        {
            return string.Format("{0}", Value);
        }
    }
}
