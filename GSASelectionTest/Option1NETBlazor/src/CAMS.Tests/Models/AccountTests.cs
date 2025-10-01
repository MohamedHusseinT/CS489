using CAMS.Shared.Models;
using Xunit;

namespace CAMS.Tests.Models
{
    public class AccountTests
    {
        [Fact]
        public void IsPrimeAccount_ReturnsTrue_WhenBalanceGreaterThan10000()
        {
            // Arrange
            var account = new Account
            {
                Balance = 15000m
            };

            // Act
            var result = account.IsPrimeAccount();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsPrimeAccount_ReturnsFalse_WhenBalanceLessThanOrEqualTo10000()
        {
            // Arrange
            var account = new Account
            {
                Balance = 10000m
            };

            // Act
            var result = account.IsPrimeAccount();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetFormattedBalance_ReturnsCorrectCurrencyFormat()
        {
            // Arrange
            var account = new Account
            {
                Balance = 1234.56m
            };

            // Act
            var result = account.GetFormattedBalance();

            // Assert
            Assert.Equal("$1,234.56", result);
        }
    }
}
