using BriquePro.Domain.Common;

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
            var money = new Money(amount, currency);

            // Assert
            Assert.NotNull(money);
            Assert.Equal(amount, money.Value);
            Assert.Equal("USD", money.Currency);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0.01)]
        [InlineData(999999.99)]
        [InlineData(999999999999.99)]
        public void Constructor_WithVariousValidAmounts_CreatesMoneyInstance(decimal validAmount)
        {
            //Act
            var money = new Money(validAmount, "EUR");

            //Assert
            Assert.Equal(validAmount, money.Value);
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
            var money = new Money(amount, currency);

            //Assert
            Assert.Equal(currency.ToUpperInvariant(), money.Currency);
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
            var money = new Money(50m, currency);

            //Assert
            Assert.Equal(currency.ToUpperInvariant(), money.Currency);
        }

        [Theory]
        [InlineData("  USD  ")]
        [InlineData(" USD  ")]
        [InlineData("   USD  ")]
        [InlineData("  USD    ")]
        public void Constructor_WithCurrencyHavingWhitespace_TrimsAndConvertsToUppercase(string currencyWithWhitespace)
        {
            //Act
            var money = new Money(100m, currencyWithWhitespace);

            //Assert
            Assert.Equal("USD", money.Currency);
        }

        [Fact]
        public void Constructor_WithZeroAmount_IsValid()
        {
            //Act
            var money = new Money(0m, "BRL");

            //Assert
            Assert.Equal(0m, money.Value);
        }

        [Fact]
        public void Constructor_WithMaximumAllowedDecimalPlaces_IsValid()
        {
            //Arrange - maximum allowed decimal places is 4
            var amountWith4Decimals = 123.4567m;

            //Act
            var money = new Money(amountWith4Decimals, "BRL");

            //Assert
            Assert.Equal(amountWith4Decimals, money.Value);

        }

        [Fact]
        public void Constructor_WithNegativeAmount_ThrowsArgumentException()
        {
            //Arrange
            var negativeAmount = -50.00m;

            //Act & Assert
            var exception = Assert.Throws<ArgumentException>(
                () => new Money(negativeAmount, "USD")
            );

            Assert.Contains("negative", exception.Message);
            Assert.Equal("amount", exception.ParamName);
        }

        [Theory]
        [InlineData(-0.01)]
        [InlineData(-1)]
        [InlineData(-1000.50)]
        [InlineData(-999999.99)]
        public void Constructor_WithVariousNegativeAmounts_ThrowsArgumentException(decimal negativeAmount)
        {
            //Act & Assert
            Assert.Throws<ArgumentException>(
                () => new Money(negativeAmount, "EUR")
            );
        }
    }
}
