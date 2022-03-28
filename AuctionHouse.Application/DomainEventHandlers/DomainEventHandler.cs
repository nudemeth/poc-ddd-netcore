using MassTransit;

namespace AuctionHouse.Application.DomainEventHandlers
{
    public abstract class DomainEventHandler<TMessage> : IConsumer<TMessage>
        where TMessage : class
    {
        public async Task Consume(ConsumeContext<TMessage> context)
        {
            await Handle(context.Message);
            await context.RespondAsync(NoReplyMessage.Instance);
        }

        protected abstract Task Handle(TMessage @event);
    }
}
