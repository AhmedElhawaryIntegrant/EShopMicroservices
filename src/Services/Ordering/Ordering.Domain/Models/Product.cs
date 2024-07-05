using Ordering.Domain.Abstraction;
using Ordering.Domain.ValueObject;

namespace Ordering.Domain.Models
{
    public class Product : Entity<ProductId>
    {
        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; private set; } = default!;

        public decimal Price { get; private set; }
    }
}
