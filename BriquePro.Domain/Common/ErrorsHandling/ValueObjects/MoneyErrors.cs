namespace BriquePro.Domain.Common.ErrorsHandling.ValueObjects
{
    public static class MoneyErrors
    {
        public static readonly Error NegativeAmount = new(
            "NegativeAmount",
            "Value cannot be negative."
        );
        public static readonly Error TooManyDecimalPlaces = new(
            "TooManyDecimalPlaces",
            $"Money cannot have more than 4 decimal places."
        );
        public static readonly Error InvalidCurrency = new(
            "InvalidCurrency",
            "Currency must be a 3-letter ISO code."
        );
    }
}
