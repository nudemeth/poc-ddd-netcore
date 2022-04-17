using FluentValidation;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.Behaviours
{
    public class ValidationBehaviour<TMessage> : IFilter<ConsumeContext<TMessage>>
        where TMessage : class
    {
        private readonly ILogger<ValidationBehaviour<TMessage>> logger;
        private readonly IEnumerable<IValidator<TMessage>> validators;

        public ValidationBehaviour(IEnumerable<IValidator<TMessage>> validators, ILogger<ValidationBehaviour<TMessage>> logger)
        {
            this.validators = validators;
            this.logger = logger;
        }

        public void Probe(ProbeContext context)
        {
            context.CreateFilterScope("validation");
        }

        public async Task Send(ConsumeContext<TMessage> context, IPipe<ConsumeContext<TMessage>> next)
        {
            logger.LogInformation("Validating --> {type}", typeof(TMessage));

            if (validators.Any())
            {
                var validationContext = new ValidationContext<TMessage>(context.Message);
                var results = await Task.WhenAll(validators.Select(v => v.ValidateAsync(validationContext, context.CancellationToken)));
                var failures = results.SelectMany(r => r.Errors).Where(r => r != null);

                if (failures.Any())
                {
                    throw new ValidationException(failures);
                }
            }

            await next.Send(context);
        }
    }
}
