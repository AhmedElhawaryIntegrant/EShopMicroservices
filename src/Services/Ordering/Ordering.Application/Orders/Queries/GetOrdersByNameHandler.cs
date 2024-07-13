

using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;

namespace Ordering.Application.Orders.Queries
{
    public class GetOrdersByNameHandler(IApplicationDBContext dBContext) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            var orders = await dBContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.OrderName.Value.Contains(query.Name))
                .OrderByDescending(o => o.OrderName)
                .ToListAsync(cancellationToken);
            //var orderDtos = ProjectToOrderDto(orders);
            var orderDtos = orders.ToOrderDtoList();
            return new GetOrdersByNameResult(orderDtos);
        }

        private List<OrderDto> ProjectToOrderDto(IEnumerable<Order> Orders)
        {
            List<OrderDto> result = new();

            foreach(var order in Orders)
            {
                var orderDto = new OrderDto
                (
                    Id : order.Id.Value,
                    CustomerId : order.CustomerId.Value,
                    OrderName : order.OrderName.Value,
                    ShippingAddress : new AddressDto
                    (
                        FirstName : order.ShippingAddress.FirstName,
                        LastName :order.ShippingAddress.LastName,
                        EmailAddress : order.ShippingAddress.EmailAddress,
                        AddressLine : order.ShippingAddress.AddressLine,
                        Country : order.ShippingAddress.Country,
                        State : order.ShippingAddress.State,
                        ZipCode : order.ShippingAddress.ZipCode
                    ),
                    BillingAddress : new AddressDto
                    (
                        FirstName : order.BillingAddress.FirstName,
                        LastName : order.BillingAddress.LastName,
                        EmailAddress : order.BillingAddress.EmailAddress,
                        AddressLine : order.BillingAddress.AddressLine,
                        Country : order.BillingAddress.Country,
                        State : order.BillingAddress.State,
                        ZipCode : order.BillingAddress.ZipCode
                    )
                   ,
                    Status: order.Status,
                    Payment : new PaymentDto
                    (
                        CardNumber : order.Payment.CardNumber,
                        CardHolderName : order.Payment.CardHolderName,
                        Expiration : order.Payment.Expiration,
                        Cvv : order.Payment.CVV,
                        PaymentMethod : order.Payment.PaymentMethod
                    ),
                    OrderItems : order.OrderItems.Select(oi => new OrderItemDto
                    (
                        OrderId : oi.OrderId.Value,
                        ProductId : oi.ProductId.Value,
                        Quantity : oi.Quantity,
                        Price : oi.Price
                     
                    )).ToList()
                );

                result.Add(orderDto);
            }

            return result;
        }
    }
}
