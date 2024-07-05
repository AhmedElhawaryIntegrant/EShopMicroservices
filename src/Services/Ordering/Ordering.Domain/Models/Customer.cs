using Ordering.Domain.Abstraction;
using Ordering.Domain.ValueObject;
namespace Ordering.Domain.Models
{
    public class Customer : Entity<CustomerId>
    {
        public Customer(string name, string email)
        {
            Name = name;
            Email = email;
        }   
        public string Name { get; private set; } = default!;

        public string Email { get; private set; } = default!;
    }
}
