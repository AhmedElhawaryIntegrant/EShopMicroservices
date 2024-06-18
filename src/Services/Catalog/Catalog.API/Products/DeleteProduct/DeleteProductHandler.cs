namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool IsDeleted);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
    public class DeleteProductHandler(IDocumentSession session,ILogger<DeleteProductHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("DeleteProductHandler.Handle called with {@Command}", command);
            var existingProduct = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (existingProduct is null)
            {
                throw new ProductNotFoundException(command.Id);
            }
            else
            {
                session.Delete(existingProduct);
                await session.SaveChangesAsync();
                return new DeleteProductResult(true);
            }
        }
    }
    
    }

