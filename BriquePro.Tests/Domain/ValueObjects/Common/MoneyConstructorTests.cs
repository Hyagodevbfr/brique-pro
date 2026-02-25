using BriquePro.Domain.Common;
using BriquePro.Domain.Common.ErrorsHandling;

namespace BriquePro.Tests.Domain.ValueObjects.Common
{
    public class MoneyConstructorTests
    {
        [Fact]
        public void Constructor_WithValidAmountAndCurrency_CreatesMoneyInstance()
        {
            // Arrange
            var amount = 99.99m;
            var currency = "USD";

            // Act
            var money = Money.Create(amount, currency);

            // Assert
            Assert.NotNull(money);
            Assert.Equal(amount, money.Value.Value);
            Assert.Equal("USD", money.Value.Currency);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0.01)]
        [InlineData(999999.99)]
        [InlineData(999999999999.99)]
        public void Constructor_WithVariousValidAmounts_CreatesMoneyInstance(decimal validAmount)
        {
            //Act
            var money = Money.Create(validAmount, "EUR");

            //Assert
            Assert.Equal(validAmount, money.Value.Value);
        }

        [Theory]
        [InlineData("USD")]
        [InlineData("EUR")]
        [InlineData("JPY")]
        [InlineData("GBP")]
        [InlineData("BRL")]
        public void Constructor_WithVariousValidCurrencies_CreatesMoneyInstance(string currency)
        {
            //Arrange
            var amount = 100.00m;

            //Act
            var money = Money.Create(amount, currency);

            //Assert
            Assert.Equal(currency.ToUpperInvariant(), money.Value.Currency);
        }

        [Theory]
        [InlineData("usd")]
        [InlineData("eUr")]
        [InlineData("jpY")]
        [InlineData("Gbp")]
        [InlineData("brL")]
        public void Constructor_WithLowercaseCurrency_ConvertsToUppercase(string currency)
        {
            //Act
            var money = Money.Create(50m, currency);

            //Assert
            Assert.Equal(currency.ToUpperInvariant(), money.Value.Currency);
        }

        [Theory]
        [InlineData("  USD  ")]
        [InlineData(" USD  ")]
        [InlineData("   USD  ")]
        [InlineData("  USD    ")]
        public void Constructor_WithCurrencyHavingWhitespace_TrimsAndConvertsToUppercase(string currencyWithWhitespace)
        {
            //Act
            var money = Money.Create(100m, currencyWithWhitespace);

            //Assert
            Assert.Equal("USD", money.Value.Currency);
        }

        [Fact]
        public void Constructor_WithZeroAmount_IsValid()
        {
            //Act
            var money = Money.Create(0m, "BRL");

            //Assert
            Assert.Equal(0m, money.Value.Value);
        }

        [Fact]
        public void Constructor_WithMaximumAllowedDecimalPlaces_IsValid()
        {
            //Arrange - maximum allowed decimal places is 4
            var amountWith4Decimals = 123.4567m;

            //Act
            var money = Money.Create(amountWith4Decimals, "BRL");

            //Assert
            Assert.Equal(amountWith4Decimals, money.Value.Value);

        }

        [Fact]
        public void Constructor_WithNegativeAmount_HasError()
        {
            //Arrange
            var invalidAmount = -1m;

            //Act
            var result = Money.Create(invalidAmount, "USD");

            //Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Error);
            Assert.Equal("Invalid.Value", ((Error)result.Error!).Code);
        }
    }
}
