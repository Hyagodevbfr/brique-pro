
namespace BriquePro.Domain.Common
{
    public sealed class Money : ValueObject
    {
        public readonly decimal Value;
        public readonly string Currency;

        const int MAX_DECIMAL_PLACES = 4;

        public Money(decimal amount, string currency)
        {
            if (amount < 0)            
                throw new ArgumentException("Value cannot be negative.", nameof(amount));

            if (decimal.Round(amount, MAX_DECIMAL_PLACES) != amount)
                throw new ArgumentException($"Money cannot have more than {MAX_DECIMAL_PLACES} decimal places.", nameof(amount));

            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency cannot be null or whitespace.", nameof(currency));

            currency = currency.Trim();

            if (currency.Length != 3)
                throw new ArgumentException("Currency must be a 3-letter code.", nameof(currency));

            if (!currency.All(char.IsLetter))
                throw new ArgumentException("Currency must only contain letters.");

            Value = amount;
            Currency = currency.ToUpperInvariant();

        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
            yield return Currency;
        }

        public override string ToString() => $"{Value:F2} {Currency.ToUpper()}";
    }
}
