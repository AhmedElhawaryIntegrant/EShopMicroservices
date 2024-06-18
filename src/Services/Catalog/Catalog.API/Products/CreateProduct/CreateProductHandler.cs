
namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, decimal Price, string ImageFile, List<string> Category) :ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero"); 
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image File is required"); 
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category File is required"); 
        }
    }

    internal class CreateProductCommandHandler(IDocumentSession session,IValidator<CreateProductCommand> validator) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            var valiidationResult = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            if (validationResult.Errors.Any())
            {
                throw new ValidationException(validationResult.Errors);
            }

            var product = command.Adapt<Product>();
          //var product = new Product
          //{

          //    Name = command.Name, 
          //    Description = command.Description, 
          //    Price = command.Price, 
          //    ImageFile = command.ImageFile, 
          //    Category = command.Category 
          //};
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
            return new CreateProductResult(product.Id);
           //throw new NotImplementedException();
        }
    }
}
