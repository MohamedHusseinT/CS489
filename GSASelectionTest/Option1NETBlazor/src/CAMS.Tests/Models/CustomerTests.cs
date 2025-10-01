using CAMS.Shared.Models;
using Xunit;

namespace CAMS.Tests.Models
{
    public class CustomerTests
    {
        [Fact]
        public void GetFullName_ReturnsCorrectFullName()
        {
            // Arrange
            var customer = new Customer
            {
                FirstName = "John",
                LastName = "Doe"
            };

            // Act
            var result = customer.GetFullName();

            // Assert
            Assert.Equal("John Doe", result);
        }

        [Fact]
        public void GetFullName_HandlesEmptyNames()
        {
            // Arrange
            var customer = new Customer
            {
                FirstName = "",
                LastName = "Doe"
            };

            // Act
            var result = customer.GetFullName();

            // Assert
            Assert.Equal("Doe", result);
        }

        [Fact]
        public void GetFullName_TrimsWhitespace()
        {
            // Arrange
            var customer = new Customer
            {
                FirstName = "  John  ",
                LastName = "  Doe  "
            };

            // Act
            var result = customer.GetFullName();

            // Assert
            Assert.Equal("John Doe", result);
        }
    }
}
