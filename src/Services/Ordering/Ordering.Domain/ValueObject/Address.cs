﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObject
{
    public record Address
    {
        public string FirstName { get; } = default!;

        public string LastName { get; } = default!;
        public string EmailAddress { get; } = default!;
        public string AddressLine { get; } = default!;

        public string Country { get; } = default!;

        public string State { get; } = default!;

        public string ZipCode { get; } = default!;
    }
}
