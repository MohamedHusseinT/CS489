using CAMS.Shared.DTOs;
using CAMS.Shared.Models;

namespace CAMS.Blazor.Services
{
    public interface ICustomerAccountService
    {
        Task<IEnumerable<CustomerAccountDto>> GetAllCustomerAccountsAsync();
        Task<IEnumerable<CustomerAccountDto>> GetPrimeCustomerAccountsAsync();
        Task<CustomerAccountDto?> GetCustomerAccountByIdAsync(long id);
        Task<CustomerAccountDto> CreateCustomerAccountAsync(CreateCustomerAccountDto dto);
        Task<LiquidityPositionDto> GetLiquidityPositionAsync();
        Task<bool> DeleteCustomerAccountAsync(long id);
    }
}
