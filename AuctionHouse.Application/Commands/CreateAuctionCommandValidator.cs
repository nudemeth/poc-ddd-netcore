using FluentValidation;

namespace AuctionHouse.Application.Commands
{
    public class CreateAuctionCommandValidator : AbstractValidator<CreateAuctionCommandRequest>
    {
        public CreateAuctionCommandValidator()
        {
            this.RuleFor(c => c.StartingPrice)
                .GreaterThanOrEqualTo(100);
        }
    }
}
