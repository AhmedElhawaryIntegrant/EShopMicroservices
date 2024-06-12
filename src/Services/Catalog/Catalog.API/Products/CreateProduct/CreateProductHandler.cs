using BuildingBlocks.CQRS;
using Catalog.API.Models;
namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, decimal Price, string ImageFile, List<string> Category) :ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    internal class CreateProductCommandHandler : ICommadHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
          var product = new Product
          {

              Name = command.Name, 
              Description = command.Description, 
              Price = command.Price, 
              ImageFile = command.ImageFile, 
              Category = command.Category 
          };

            return new CreateProductResult(Guid.NewGuid());
           //throw new NotImplementedException();
        }
    }
}
