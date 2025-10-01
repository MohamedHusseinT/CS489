using Microsoft.EntityFrameworkCore;
using CAMS.Shared.Data;
using CAMS.Shared.DTOs;
using CAMS.Shared.Models;

namespace CAMS.Blazor.Services
{
    public class CustomerAccountService : ICustomerAccountService
    {
        private readonly CamsDbContext _context;

        public CustomerAccountService(CamsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerAccountDto>> GetAllCustomerAccountsAsync()
        {
            var accounts = await _context.Accounts
                .Include(a => a.Customer)
                .Select(a => new CustomerAccountDto
                {
                    AccountId = a.AccountId,
                    AccountNumber = a.AccountNumber,
                    AccountType = a.AccountType,
                    DateOpened = a.DateOpened,
                    Balance = a.Balance,
                    CustomerId = a.CustomerId,
                    CustomerName = a.Customer.GetFullName(),
                    FirstName = a.Customer.FirstName,
                    LastName = a.Customer.LastName
                })
                .ToListAsync();

            // Sort by balance descending on client side (SQLite doesn't support decimal ORDER BY)
            return accounts.OrderByDescending(a => a.Balance);
        }

        public async Task<IEnumerable<CustomerAccountDto>> GetPrimeCustomerAccountsAsync()
        {
            var accounts = await _context.Accounts
                .Include(a => a.Customer)
                .Where(a => a.Balance > 10000)
                .Select(a => new CustomerAccountDto
                {
                    AccountId = a.AccountId,
                    AccountNumber = a.AccountNumber,
                    AccountType = a.AccountType,
                    DateOpened = a.DateOpened,
                    Balance = a.Balance,
                    CustomerId = a.CustomerId,
                    CustomerName = a.Customer.GetFullName(),
                    FirstName = a.Customer.FirstName,
                    LastName = a.Customer.LastName
                })
                .ToListAsync();

            // Sort by balance descending on client side (SQLite doesn't support decimal ORDER BY)
            return accounts.OrderByDescending(a => a.Balance);
        }

        public async Task<CustomerAccountDto?> GetCustomerAccountByIdAsync(long id)
        {
            var account = await _context.Accounts
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(a => a.AccountId == id);

            if (account == null)
                return null;

            return new CustomerAccountDto
            {
                AccountId = account.AccountId,
                AccountNumber = account.AccountNumber,
                AccountType = account.AccountType,
                DateOpened = account.DateOpened,
                Balance = account.Balance,
                CustomerId = account.CustomerId,
                CustomerName = account.Customer.GetFullName(),
                FirstName = account.Customer.FirstName,
                LastName = account.Customer.LastName
            };
        }

        public async Task<CustomerAccountDto> CreateCustomerAccountAsync(CreateCustomerAccountDto dto)
        {
            // Create customer
            var customer = new Customer
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            // Create account
            var account = new Account
            {
                AccountNumber = dto.AccountNumber,
                AccountType = dto.AccountType,
                DateOpened = dto.DateOpened,
                Balance = dto.Balance,
                CustomerId = customer.CustomerId
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return new CustomerAccountDto
            {
                AccountId = account.AccountId,
                AccountNumber = account.AccountNumber,
                AccountType = account.AccountType,
                DateOpened = account.DateOpened,
                Balance = account.Balance,
                CustomerId = account.CustomerId,
                CustomerName = customer.GetFullName(),
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };
        }

        public async Task<LiquidityPositionDto> GetLiquidityPositionAsync()
        {
            var accounts = await _context.Accounts.ToListAsync();
            
            return new LiquidityPositionDto
            {
                TotalBalance = accounts.Sum(a => a.Balance),
                TotalAccounts = accounts.Count,
                PrimeAccounts = accounts.Count(a => a.Balance > 10000)
            };
        }

        public async Task<bool> DeleteCustomerAccountAsync(long id)
        {
            var account = await _context.Accounts
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(a => a.AccountId == id);

            if (account == null)
                return false;

            _context.Accounts.Remove(account);
            _context.Customers.Remove(account.Customer);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
