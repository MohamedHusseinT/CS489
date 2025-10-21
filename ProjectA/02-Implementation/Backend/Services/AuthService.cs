using Microsoft.EntityFrameworkCore;
using ADSDentalSurgeriesWebAPI.Data;
using ADSDentalSurgeriesWebAPI.Models;
using ADSDentalSurgeriesWebAPI.DTOs;

namespace ADSDentalSurgeriesWebAPI.Services
{
    public class AuthService
    {
        private readonly ADSDbContext _context;
        private readonly JwtService _jwtService;

        public AuthService(ADSDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            try
            {
                // Find user by username or email
                var user = await _context.Users
                    .Include(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                    .FirstOrDefaultAsync(u => 
                        u.Username == request.UsernameOrEmail || 
                        u.Email == request.UsernameOrEmail);

                if (user == null)
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Message = "Invalid username or password"
                    };
                }

                // Check if user is active
                if (!user.IsActive)
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Message = "Account is disabled"
                    };
                }

                // Verify password
                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Message = "Invalid username or password"
                    };
                }

                // Get user roles
                var roles = user.UserRoles.Select(ur => ur.Role.RoleName).ToList();

                // Generate JWT token
                var token = _jwtService.GenerateToken(user, roles);
                var expiresAt = DateTime.UtcNow.AddHours(24);

                // Update last login date
                user.LastLoginDate = DateTime.Now;
                await _context.SaveChangesAsync();

                return new AuthResponse
                {
                    Success = true,
                    Message = "Login successful",
                    Token = token,
                    ExpiresAt = expiresAt,
                    User = new UserInfo
                    {
                        UserId = user.UserId,
                        Username = user.Username,
                        Email = user.Email,
                        FullName = user.FullName,
                        Roles = roles
                    }
                };
            }
            catch (Exception ex)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = $"Login failed: {ex.Message}"
                };
            }
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            try
            {
                // Check if username already exists
                if (await _context.Users.AnyAsync(u => u.Username == request.Username))
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Message = "Username already exists"
                    };
                }

                // Check if email already exists
                if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Message = "Email already exists"
                    };
                }

                // Hash password
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                // Create new user
                var user = new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Assign default USER role
                var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == "USER");
                if (userRole != null)
                {
                    _context.UserRoles.Add(new UserRole
                    {
                        UserId = user.UserId,
                        RoleId = userRole.RoleId,
                        AssignedDate = DateTime.Now
                    });
                    await _context.SaveChangesAsync();
                }

                return new AuthResponse
                {
                    Success = true,
                    Message = "Registration successful. Please login.",
                    User = new UserInfo
                    {
                        UserId = user.UserId,
                        Username = user.Username,
                        Email = user.Email,
                        FullName = user.FullName,
                        Roles = new List<string> { "USER" }
                    }
                };
            }
            catch (Exception ex)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = $"Registration failed: {ex.Message}"
                };
            }
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<List<string>> GetUserRolesAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            return user?.UserRoles.Select(ur => ur.Role.RoleName).ToList() ?? new List<string>();
        }
    }
}

