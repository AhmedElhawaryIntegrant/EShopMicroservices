using Shopping.Web.Models.Ordering;

namespace Shopping.Web.Services
{
    public interface IOrderingService
    {
        [Get("/ordering-service/orders?PageIndex={PageIndex}&PageSize={PageSize}")]
        Task<GetOrdersResponse> GetOrders(int PageIndex,int PageSize);

        [Get("/ordering-service/orders/{orderName}")]
        Task<GetOrderByNameResponse> GetOrdersByName(string orderName);

        [Get("/ordering-service/orders/customer/{customerId}")]
        Task<GetOrderByResponse> GetOrdersByCustomer(Guid customerId);
    }
}
