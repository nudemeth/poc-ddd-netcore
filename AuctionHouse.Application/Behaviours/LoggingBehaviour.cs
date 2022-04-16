using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.Behaviours
{
    public class LoggingBehaviour<TMessage> : IFilter<SendContext<TMessage>>
        where TMessage : class
    {
        private readonly ILogger<LoggingBehaviour<TMessage>> logger;

        public LoggingBehaviour(ILogger<LoggingBehaviour<TMessage>> logger)
        {
            this.logger = logger;
        }

        public void Probe(ProbeContext context)
        {
            context.CreateFilterScope("logging");
        }

        public async Task Send(SendContext<TMessage> context, IPipe<SendContext<TMessage>> next)
        {
            try
            {
                logger.LogInformation("{type} --> {message}", typeof(TMessage), context.Message);
                await next.Send(context);
            }
            catch (System.Exception ex)
            {
                logger.LogError(ex, "{type} ::: {message}", typeof(TMessage), ex.Message);
            }
        }
    }
}
