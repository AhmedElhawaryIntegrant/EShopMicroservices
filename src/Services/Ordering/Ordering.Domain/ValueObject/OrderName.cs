using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObject
{
    public record OrderName
    {
        private const int DefaultLength = 5;
        public string Value { get; } = default!;

        private OrderName(string value) => Value = value;

        public static OrderName Of(string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength, "Order Name");
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException("Order Name cannot be empty");
            }

            return new OrderName(value);
        }

        
    }
}
