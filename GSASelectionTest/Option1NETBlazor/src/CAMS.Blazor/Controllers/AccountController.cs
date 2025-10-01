using Microsoft.AspNetCore.Mvc;
using CAMS.Blazor.Services;
using CAMS.Shared.DTOs;

namespace CAMS.Blazor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ICustomerAccountService _customerAccountService;

        public AccountController(ICustomerAccountService customerAccountService)
        {
            _customerAccountService = customerAccountService;
        }

        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<CustomerAccountDto>>> GetAllAccounts()
        {
            try
            {
                var accounts = await _customerAccountService.GetAllCustomerAccountsAsync();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("prime/list")]
        public async Task<ActionResult<IEnumerable<CustomerAccountDto>>> GetPrimeAccounts()
        {
            try
            {
                var primeAccounts = await _customerAccountService.GetPrimeCustomerAccountsAsync();
                return Ok(primeAccounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerAccountDto>> GetAccount(long id)
        {
            try
            {
                var account = await _customerAccountService.GetCustomerAccountByIdAsync(id);
                if (account == null)
                {
                    return NotFound($"Account with ID {id} not found.");
                }
                return Ok(account);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CustomerAccountDto>> CreateAccount([FromBody] CreateCustomerAccountDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var account = await _customerAccountService.CreateCustomerAccountAsync(dto);
                return CreatedAtAction(nameof(GetAccount), new { id = account.AccountId }, account);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAccount(long id)
        {
            try
            {
                var result = await _customerAccountService.DeleteCustomerAccountAsync(id);
                if (!result)
                {
                    return NotFound($"Account with ID {id} not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("liquidity")]
        public async Task<ActionResult<LiquidityPositionDto>> GetLiquidityPosition()
        {
            try
            {
                var liquidity = await _customerAccountService.GetLiquidityPositionAsync();
                return Ok(liquidity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
