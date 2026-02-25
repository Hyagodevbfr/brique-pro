using BriquePro.Domain.Common.ErrorsHandling;

namespace BriquePro.Domain.Common
{
    public sealed class Money : ValueObject
    {
        public readonly decimal Value;
        public readonly string Currency;

        const int MAX_DECIMAL_PLACES = 4;

        private Money() { }

        private Money(decimal amount, string currency)
        {
            Value = amount;
            Currency = currency;
        }

        public static Result<Money> Create(decimal amount, string currency)
        {
            if (amount < 0)
                return Result<Money>.Failure(new Error("Invalid.Value", "Amount cannot be negative."));

            if (decimal.Round(amount, MAX_DECIMAL_PLACES) != amount)
                return Result<Money>.Failure(new Error("Invalid.Value", $"Amount cannot have more than {MAX_DECIMAL_PLACES} decimal places."));

            if (string.IsNullOrWhiteSpace(currency))
                return Result<Money>.Failure(new Error("Invalid.Currency", "Currency cannot be null or whitespace."));

            currency = currency.Trim().ToUpperInvariant();

            if (currency.Length != 3 || !currency.All(char.IsLetter))
                return Result<Money>.Failure(new Error("Invalid.Currency", "Currency must be a 3-letter ISO code."));
            
    
            return new Money(amount, currency);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
            yield return Currency;
        }

        public override string ToString() => $"{Value:F2} {Currency.ToUpper()}";
    }
}
