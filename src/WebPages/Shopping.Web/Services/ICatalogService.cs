namespace Shopping.Web.Services
{
    public interface ICatalogService
    {
        [Get("/catalog-service/products?pageNumber={PageNumber}&pageSize={PageSize}")]
        Task<GetProductsResponse> GetProducts(int? PageNumber = 1, int? PageSize = 10);

        [Get("/catalog-service/products/{id}")]
        Task<GetProductByIdResponse> GetProduct(Guid id);
        [Get("/catalog-service/products/{category}")]
        Task<GetProductsByCategoryResponse> GetProductsByCategory(string category);
    }
}
