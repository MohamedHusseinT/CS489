using Microsoft.EntityFrameworkCore;
using CAMS.Shared.Data;
using CAMS.Shared.DTOs;
using CAMS.Blazor.Services;
using Xunit;

namespace CAMS.Tests.Services
{
    public class CustomerAccountServiceTests : IDisposable
    {
        private readonly CamsDbContext _context;
        private readonly CustomerAccountService _service;

        public CustomerAccountServiceTests()
        {
            var options = new DbContextOptionsBuilder<CamsDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new CamsDbContext(options);
            _service = new CustomerAccountService(_context);
        }

        [Fact]
        public async Task GetAllCustomerAccountsAsync_ReturnsAllAccountsSortedByBalanceDescending()
        {
            // Arrange
            await SeedTestData();

            // Act
            var result = await _service.GetAllCustomerAccountsAsync();

            // Assert
            Assert.Equal(3, result.Count());
            var accounts = result.ToList();
            Assert.Equal(15000m, accounts[0].Balance); // AC1003
            Assert.Equal(10900.50m, accounts[1].Balance); // AC1002
            Assert.Equal(125.95m, accounts[2].Balance); // AC1001
        }

        [Fact]
        public async Task GetPrimeCustomerAccountsAsync_ReturnsOnlyAccountsWithBalanceGreaterThan10000()
        {
            // Arrange
            await SeedTestData();

            // Act
            var result = await _service.GetPrimeCustomerAccountsAsync();

            // Assert
            Assert.Equal(2, result.Count());
            var accounts = result.ToList();
            Assert.True(accounts.All(a => a.Balance > 10000));
            Assert.Contains(accounts, a => a.AccountNumber == "AC1002");
            Assert.Contains(accounts, a => a.AccountNumber == "AC1003");
        }

        [Fact]
        public async Task GetCustomerAccountByIdAsync_ReturnsCorrectAccount()
        {
            // Arrange
            await SeedTestData();

            // Act
            var result = await _service.GetCustomerAccountByIdAsync(1L); // Use long instead of int

            // Assert
            Assert.NotNull(result);
            Assert.Equal("AC1002", result.AccountNumber);
            Assert.Equal("Anna Smith", result.CustomerName);
            Assert.Equal(10900.50m, result.Balance);
        }

        [Fact]
        public async Task GetCustomerAccountByIdAsync_ReturnsNullForNonExistentAccount()
        {
            // Arrange
            await SeedTestData();

            // Act
            var result = await _service.GetCustomerAccountByIdAsync(999L); // Use long instead of int

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateCustomerAccountAsync_CreatesNewCustomerAndAccount()
        {
            // Arrange
            var dto = new CreateCustomerAccountDto
            {
                FirstName = "John",
                LastName = "Doe",
                AccountNumber = "AC1004",
                AccountType = "Checking",
                Balance = 5000m,
                DateOpened = DateTime.Now
            };

            // Act
            var result = await _service.CreateCustomerAccountAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("AC1004", result.AccountNumber);
            Assert.Equal("John Doe", result.CustomerName);
            Assert.Equal(5000m, result.Balance);

            // Verify in database
            var account = await _context.Accounts
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(a => a.AccountNumber == "AC1004");
            Assert.NotNull(account);
            Assert.Equal("John", account.Customer.FirstName);
            Assert.Equal("Doe", account.Customer.LastName);
        }

        [Fact]
        public async Task GetLiquidityPositionAsync_ReturnsCorrectTotals()
        {
            // Arrange
            await SeedTestData();

            // Act
            var result = await _service.GetLiquidityPositionAsync();

            // Assert
            Assert.Equal(26026.45m, result.TotalBalance); // 10900.50 + 125.95 + 15000
            Assert.Equal(3, result.TotalAccounts);
            Assert.Equal(2, result.PrimeAccounts);
        }

        [Fact]
        public async Task DeleteCustomerAccountAsync_RemovesAccountAndCustomer()
        {
            // Arrange
            await SeedTestData();
            var accountId = 1L; // Use long instead of int

            // Act
            var result = await _service.DeleteCustomerAccountAsync(accountId);

            // Assert
            Assert.True(result);
            var account = await _context.Accounts.FindAsync(accountId);
            Assert.Null(account);
            
            // Customer should also be deleted due to cascade
            var customer = await _context.Customers.FindAsync(2L); // Use long instead of int
            Assert.Null(customer);
        }

        [Fact]
        public async Task DeleteCustomerAccountAsync_ReturnsFalseForNonExistentAccount()
        {
            // Arrange
            await SeedTestData();

            // Act
            var result = await _service.DeleteCustomerAccountAsync(999L); // Use long instead of int

            // Assert
            Assert.False(result);
        }

        private async Task SeedTestData()
        {
            // Add test customers
            var customers = new[]
            {
                new CAMS.Shared.Models.Customer { CustomerId = 1, FirstName = "Bob", LastName = "Jones" },
                new CAMS.Shared.Models.Customer { CustomerId = 2, FirstName = "Anna", LastName = "Smith" },
                new CAMS.Shared.Models.Customer { CustomerId = 3, FirstName = "Carlos", LastName = "Jimenez" }
            };

            _context.Customers.AddRange(customers);

            // Add test accounts
            var accounts = new[]
            {
                new CAMS.Shared.Models.Account 
                { 
                    AccountId = 1, 
                    AccountNumber = "AC1002", 
                    AccountType = "Checking", 
                    DateOpened = new DateTime(2022, 7, 10), 
                    Balance = 10900.50m, 
                    CustomerId = 2 
                },
                new CAMS.Shared.Models.Account 
                { 
                    AccountId = 2, 
                    AccountNumber = "AC1001", 
                    AccountType = "Savings", 
                    DateOpened = new DateTime(2021, 11, 15), 
                    Balance = 125.95m, 
                    CustomerId = 1 
                },
                new CAMS.Shared.Models.Account 
                { 
                    AccountId = 3, 
                    AccountNumber = "AC1003", 
                    AccountType = "Savings", 
                    DateOpened = new DateTime(2022, 7, 11), 
                    Balance = 15000m, 
                    CustomerId = 3 
                }
            };

            _context.Accounts.AddRange(accounts);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
