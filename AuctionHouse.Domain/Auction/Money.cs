using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AuctionHouse.Domain.Auction
{
    public sealed record Money : ValueObject<Money>
    {
        public decimal Value { get; }

        public Money()
            : this(0m)
        {
        }

        public Money(Money money)
            : base(money)
        {
            this.Value = money.Value;
        }

        [JsonConstructor]
        public Money(decimal value)
        {
            if (value % 0.01m != 0)
                throw new MoreThanTwoDecimalPlacesInMoneyValueException();

            if (value < 0)
                throw new MoneyCannotBeANegativeValueException();

            this.Value = value;
        }

        public Money Add(Money money)
        {
            return new Money(Value + money.Value);
        }

        public bool IsGreaterThan(Money money)
        {
            return this.Value > money.Value;
        }

        public bool IsGreaterThanOrEqualTo(Money money)
        {
            return this.Value > money.Value || this.Equals(money);
        }

        public bool IsLessThanOrEqualTo(Money money)
        {
            return this.Value < money.Value || this.Equals(money);
        }

        public override string ToString()
        {
            return string.Format("{0}", Value);
        }
    }
}
