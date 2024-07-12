using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderResult(Guid OrderId);
    public record CreateOrderCommand(OrderDto Order):ICommand<CreateOrderResult>;

    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
           RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Order Name is required");
           RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("Order Items should not be empty");
           RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("CustomerId is required");
        }
    }
   
}
