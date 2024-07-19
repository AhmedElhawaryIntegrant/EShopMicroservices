
using Basket.API.Data;
using Mapster;
using MassTransit;
using BuildingBlocks.Messaging.Events;

namespace Basket.API.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;
    public record CheckoutBasketResult(bool IsSuccess);

    public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckout can't be null");
            RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }
    public class CheckoutBasketHandler(IBasketRepository repository,IPublishEndpoint publishEndPoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasket(command.BasketCheckoutDto.UserName);
            if ((basket is null))
            {
                return new CheckoutBasketResult(false);
            }
            var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;
            await publishEndPoint.Publish(eventMessage, cancellationToken);
            await repository.DeleteBasket(command.BasketCheckoutDto.UserName);
            return new CheckoutBasketResult(true);
        }
    }
}
