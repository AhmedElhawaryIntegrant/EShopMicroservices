namespace Shopping.Web.Models.Ordering
{
    public record class AddressModel(string FirstName, string LastName, string? EmailAddress, string AddressLine, string Country, string State, string ZipCode);
    public record class PaymentModel(string CardNumber, string CardHolderName, string Expiration, string Cvv, int PaymentMethod);
    public record OrderItemModel(Guid OrderId, Guid ProductId, int Quantity, decimal Price);
    public enum OrderStatus
    {
        Draft = 1,
        Pending = 2,
        Completed = 3,
        Canceled = 4
    }

    public record OrderModel(Guid Id,
    Guid CustomerId,
    string OrderName,
    AddressModel ShippingAddress,
    AddressModel BillingAddress,
    PaymentModel Payment,
    OrderStatus Status,
    List<OrderItemModel> OrderItems);

    public record GetOrdersResponse(PaginatedResult<OrderModel> Orders);

    public record GetOrderByNameResponse(IEnumerable<OrderModel> orders);

    public record GetOrderByResponse(IEnumerable<OrderModel> orders);
}
