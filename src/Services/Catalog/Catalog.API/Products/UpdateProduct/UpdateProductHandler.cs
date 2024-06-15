using BuildingBlocks.CQRS;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid id ,string Name, string Description, decimal Price, string ImageFile, List<string> Category):ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);
    
    public class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductHandler.Handle called with {@Command}", command);
            var existingProduct = await session.LoadAsync<Product>(command.id, cancellationToken);
            if (existingProduct is null) 
            { 

                throw new ProductNotFoundException(command.id);
            }
            else
            {
                existingProduct.Name = command.Name;
                existingProduct.Description = command.Description;
                existingProduct.Price = command.Price;
                existingProduct.ImageFile = command.ImageFile;
                existingProduct.Category = command.Category;

                session.Update(existingProduct);

                await session.SaveChangesAsync();

                return new UpdateProductResult(true);
            }

        }
    }
}
