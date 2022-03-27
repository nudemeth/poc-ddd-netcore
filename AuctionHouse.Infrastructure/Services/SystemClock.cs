using AuctionHouse.Application.Services;

namespace AuctionHouse.Infrastructure.Services
{
    public class SystemClock : IClock
    {
        public DateTimeOffset Time()
        {
            return DateTimeOffset.UtcNow;
        }
    }
}
