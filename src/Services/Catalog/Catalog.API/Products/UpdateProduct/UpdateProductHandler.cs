using BuildingBlocks.CQRS;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid id ,string Name, string Description, decimal Price, string ImageFile, List<string> Category):ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);
    
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").Length(2,150).WithMessage("Name Length should between 2 and 150 character");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero"); 
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image File is required"); 
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category File is required"); 
        }
    }
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
