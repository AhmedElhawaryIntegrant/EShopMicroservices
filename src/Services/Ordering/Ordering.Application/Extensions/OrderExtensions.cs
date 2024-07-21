using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Extensions
{
    public static  class OrderExtensions
    {
        public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> Orders)
        {
            return Orders.Select(order => new OrderDto(
              Id: order.Id.Value,
              CustomerId: order.CustomerId.Value,
              OrderName: order.OrderName.Value,
              ShippingAddress: new AddressDto(
                  FirstName: order.ShippingAddress.FirstName,
                  LastName: order.ShippingAddress.LastName,
                  EmailAddress: order.ShippingAddress.EmailAddress,
                  AddressLine: order.ShippingAddress.AddressLine,
                  Country: order.ShippingAddress.Country,
                  State: order.ShippingAddress.State,
                  ZipCode: order.ShippingAddress.ZipCode
                ),
                BillingAddress: new AddressDto(
                  FirstName: order.BillingAddress.FirstName,
                  LastName: order.BillingAddress.LastName,
                  EmailAddress: order.BillingAddress.EmailAddress,
                  AddressLine: order.BillingAddress.AddressLine,
                  Country: order.BillingAddress.Country,
                  State: order.BillingAddress.State,
                  ZipCode: order.BillingAddress.ZipCode
                ),
                     Status: order.Status,
                    Payment: new PaymentDto
                    (
                        CardNumber: order.Payment.CardNumber,
                        CardHolderName: order.Payment.CardHolderName,
                        Expiration: order.Payment.Expiration,
                        Cvv: order.Payment.CVV,
                        PaymentMethod: order.Payment.PaymentMethod
                    ),

                    OrderItems: order.OrderItems.Select(oi => new OrderItemDto(OrderId: oi.OrderId.Value, ProductId: oi.ProductId.Value
                    , Quantity: oi.Quantity, Price: oi.Price)).ToList()
                    ));
                  
                    
             
        }

        public static OrderDto ToOrderDto(this Order order)
        {
            return DtoFromOrder(order);
        }
        private static OrderDto DtoFromOrder(Order order)
        {             return new OrderDto(
                Id: order.Id.Value,
                CustomerId: order.CustomerId.Value,
                OrderName: order.OrderName.Value,
                ShippingAddress: new AddressDto(
                    FirstName: order.ShippingAddress.FirstName,
                    LastName: order.ShippingAddress.LastName,
                    EmailAddress: order.ShippingAddress.EmailAddress,
                    AddressLine: order.ShippingAddress.AddressLine,
                    Country: order.ShippingAddress.Country,
                    State: order.ShippingAddress.State,
                    ZipCode: order.ShippingAddress.ZipCode
                ),
                BillingAddress: new AddressDto(
                    FirstName: order.BillingAddress.FirstName,
                    LastName: order.BillingAddress.LastName,
                    EmailAddress: order.BillingAddress.EmailAddress,
                    AddressLine: order.BillingAddress.AddressLine,
                    Country: order.BillingAddress.Country,
                    State: order.BillingAddress.State,
                    ZipCode: order.BillingAddress.ZipCode
                ),
                Status: order.Status,
                Payment: new PaymentDto
                (
                    CardNumber: order.Payment.CardNumber,
                    CardHolderName: order.Payment.CardHolderName,
                    Expiration: order.Payment.Expiration,
                    Cvv: order.Payment.CVV,
                    PaymentMethod: order.Payment.PaymentMethod
                ),

                OrderItems: order.OrderItems.Select(oi => new OrderItemDto(OrderId: oi.OrderId.Value, ProductId: oi.ProductId.Value
                , Quantity: oi.Quantity, Price: oi.Price)).ToList()
            );
        }
    }
}
