using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObject
{
    public record Payment
    {
        public string CardNumber { get; } = default!;

        public string CardHolderName { get; } = default!;

        public DateTime Expiration { get; } = default!;

        public string CVV { get; } = default!;

        public int PaymentMethod { get; } = default!;
    }
}
