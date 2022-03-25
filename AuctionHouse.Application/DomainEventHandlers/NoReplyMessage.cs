namespace AuctionHouse.Application.DomainEventHandlers
{
    public class NoReplyMessage
    {
        private NoReplyMessage()
        {
        }

        public static NoReplyMessage Instance { get; } = new NoReplyMessage();
    }
}
