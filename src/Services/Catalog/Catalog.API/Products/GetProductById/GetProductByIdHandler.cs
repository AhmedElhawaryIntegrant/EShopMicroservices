namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdRequest(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    public class GetProductByIdHandler(IDocumentSession session,ILogger<GetProductByIdHandler> logger) : IQueryHandler<GetProductByIdRequest, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
        {
                 logger.LogInformation("GetProductByIdHandler.Handle");
            var product = await session.LoadAsync<Product>(request.Id, cancellationToken);

            if (product is null)
                throw new ProductNotFoundException(request.Id);

            return new GetProductByIdResult(product);
        }
    }
   
}
