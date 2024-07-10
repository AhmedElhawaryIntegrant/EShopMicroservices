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

        public string Expiration { get; } = default!;

        public string CVV { get; } = default!;

        public int PaymentMethod { get; } = default!;

        protected Payment()
        {
        }

        private Payment(string cardNumber, string cardHolderName, string expiration, string cvv, int paymentMethod)
        {
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            Expiration = expiration;
            CVV = cvv;
            PaymentMethod = paymentMethod;
        }

        public static Payment Of(string cardNumber, string cardHolderName, string expiration, string cvv, int paymentMethod)
        {
            ArgumentNullException.ThrowIfNull(cardNumber);
            ArgumentNullException.ThrowIfNull(cardHolderName);
            ArgumentNullException.ThrowIfNull(expiration);
            ArgumentNullException.ThrowIfNull(cvv);
            ArgumentNullException.ThrowIfNull(paymentMethod);

            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                throw new DomainException("Card Number cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(cardHolderName))
            {
                throw new DomainException("Card Holder Name cannot be empty");
            }

            if (expiration == default)
            {
                throw new DomainException("Expiration cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(cvv))
            {
                throw new DomainException("CVV cannot be empty");
            }

            if (cvv.Length > 3)
            {
                throw new DomainException("CVV shold not be greater than zero");
            }

            if (paymentMethod == default)
            {
                throw new DomainException("Payment Method cannot be empty");
            }

            return new Payment(cardNumber, cardHolderName, expiration, cvv, paymentMethod);
        }
    }
}

