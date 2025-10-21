using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ADSDentalSurgeriesWebAPI.Services;
using ADSDentalSurgeriesWebAPI.DTOs;

namespace ADSDentalSurgeriesWebAPI.Controllers
{
    /// <summary>
    /// REST API Controller for Authentication operations
    /// </summary>
    [ApiController]
    [Route("adsweb/api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Login endpoint
        /// POST: adsweb/api/v1/auth/login
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponse
                {
                    Success = false,
                    Message = "Invalid request data"
                });
            }

            var response = await _authService.LoginAsync(request);

            if (!response.Success)
            {
                return Unauthorized(response);
            }

            _logger.LogInformation($"User {request.UsernameOrEmail} logged in successfully");
            return Ok(response);
        }

        /// <summary>
        /// Register endpoint
        /// POST: adsweb/api/v1/auth/register
        /// </summary>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponse
                {
                    Success = false,
                    Message = "Invalid request data"
                });
            }

            var response = await _authService.RegisterAsync(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            _logger.LogInformation($"User {request.Username} registered successfully");
            return CreatedAtAction(nameof(Register), response);
        }

        /// <summary>
        /// Get current user info
        /// GET: adsweb/api/v1/auth/me
        /// </summary>
        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UserInfo>> GetCurrentUser()
        {
            try
            {
                var userIdClaim = User.FindFirst("userId");
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                {
                    return Unauthorized(new { message = "Invalid token" });
                }

                var user = await _authService.GetUserByIdAsync(userId);
                if (user == null)
                {
                    return NotFound(new { message = "User not found" });
                }

                var roles = await _authService.GetUserRolesAsync(userId);

                return Ok(new UserInfo
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Email = user.Email,
                    FullName = user.FullName,
                    Roles = roles
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting current user");
                return StatusCode(500, new { message = "An error occurred while retrieving user information" });
            }
        }

        /// <summary>
        /// Health check endpoint (no authentication required)
        /// GET: adsweb/api/v1/auth/health
        /// </summary>
        [HttpGet("health")]
        [AllowAnonymous]
        public IActionResult Health()
        {
            return Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
        }
    }
}

