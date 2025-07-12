using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.Authentication.Commands;
using EnglishVocab.Constants.Constant;
using EnglishVocab.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EnglishVocab.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            ITokenService tokenService,
            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _logger = logger;
        }

        // Các phương thức khác giữ nguyên

        public async Task<TokenDto> RefreshTokenAsync(string accessToken, string refreshToken)
        {
            // Kiểm tra và xác thực access token hết hạn
            ClaimsPrincipal principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                throw new Exception("Invalid access token");
            }

            string userIdString = principal.FindFirstValue("uid");
            
            var user = await _userManager.FindByIdAsync(userIdString);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new Exception("Invalid refresh token or token expired");
            }

            // Tạo token mới
            var jwtSecurityToken = await _tokenService.GenerateAccessToken(user);
            var newAccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            // Tạo refresh token mới
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            
            // Cập nhật refresh token mới
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return new TokenDto
            {
                Token = newAccessToken,
                Expiration = jwtSecurityToken.ValidTo,
                Claims = principal.Claims
            };
        }

        public async Task<AuthResponse> Login(LoginCommand request)
        {
            // Tìm người dùng theo email
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new AuthResponse
                {
                    IsAuthenticated = false,
                    Message = "Không tìm thấy tài khoản với email này."
                };
            }

            // Kiểm tra mật khẩu
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                // Cập nhật thời gian đăng nhập cuối
                user.UpdatedAt = DateTime.UtcNow;
                await _userManager.UpdateAsync(user);

                // Lấy danh sách roles của người dùng
                var userRoles = await _userManager.GetRolesAsync(user);

                // Tạo JWT token
                var jwtSecurityToken = await _tokenService.GenerateAccessToken(user);
                var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                // Tạo refresh token
                var refreshToken = _tokenService.GenerateRefreshToken();
                
                // Lưu refresh token vào database
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); // Giả sử refresh token có hiệu lực 7 ngày
                await _userManager.UpdateAsync(user);

                return new AuthResponse
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Roles = userRoles.ToArray(),
                    IsAuthenticated = true,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    Message = "Đăng nhập thành công."
                };
            }
            else if (result.IsLockedOut)
            {
                return new AuthResponse
                {
                    IsAuthenticated = false,
                    Message = "Tài khoản đã bị khóa. Vui lòng thử lại sau."
                };
            }
            else
            {
                return new AuthResponse
                {
                    IsAuthenticated = false,
                    Message = "Đăng nhập thất bại. Vui lòng kiểm tra lại thông tin đăng nhập."
                };
            }
        }

        public async Task<AuthResponse> Register(RegisterCommand request)
        {
            // Kiểm tra email đã tồn tại
            var existingUserByEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingUserByEmail != null)
            {
                return new AuthResponse
                {
                    IsAuthenticated = false,
                    Message = "Email đã được sử dụng."
                };
            }

            // Kiểm tra username đã tồn tại
            var existingUserByUsername = await _userManager.FindByNameAsync(request.Username);
            if (existingUserByUsername != null)
            {
                return new AuthResponse
                {
                    IsAuthenticated = false,
                    Message = "Tên người dùng đã được sử dụng."
                };
            }

            // Tạo người dùng mới
            var user = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailConfirmed = true, // Trong thực tế, nên có xác nhận email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                // Xác định role cần thêm cho người dùng
                string roleName = UserRole.User.ToString();
                
                // Kiểm tra xem role đã tồn tại chưa
                var roleExists = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    // Nếu role chưa tồn tại, tạo mới
                    var roleResult = await _roleManager.CreateAsync(new ApplicationRole(roleName)
                    {
                        Description = $"{roleName} role"
                    });
                    
                    if (!roleResult.Succeeded)
                    {
                        _logger.LogError("Failed to create role {RoleName}: {Errors}", 
                            roleName, string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                    }
                }
                
                // Thêm người dùng vào role
                await _userManager.AddToRoleAsync(user, roleName);

                // Tạo token trực tiếp
                var userRoles = await _userManager.GetRolesAsync(user);
                var jwtSecurityToken = await _tokenService.GenerateAccessToken(user);
                var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                var refreshToken = _tokenService.GenerateRefreshToken();
                
                // Lưu refresh token vào database
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                await _userManager.UpdateAsync(user);

                return new AuthResponse
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Roles = userRoles.ToArray(),
                    IsAuthenticated = true,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    Message = "Đăng ký thành công."
                };
            }
            else
            {
                return new AuthResponse
                {
                    IsAuthenticated = false,
                    Message = "Đăng ký thất bại: " + string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }
        }

        public async Task<AuthResponse> RefreshToken(RefreshTokenCommand request)
        {
            // Kiểm tra và xác thực access token hết hạn
            ClaimsPrincipal principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
            if (principal == null)
            {
                return new AuthResponse
                {
                    IsAuthenticated = false,
                    Message = "Invalid access token"
                };
            }

            string userIdString = principal.FindFirstValue("uid");
            
            var user = await _userManager.FindByIdAsync(userIdString);

            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return new AuthResponse
                {
                    IsAuthenticated = false,
                    Message = "Invalid refresh token or token expired"
                };
            }

            // Tạo token mới
            var jwtSecurityToken = await _tokenService.GenerateAccessToken(user);
            var newAccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            // Tạo refresh token mới
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            
            // Cập nhật refresh token mới
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            // Lấy thông tin role
            var userRoles = await _userManager.GetRolesAsync(user);

            return new AuthResponse
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = userRoles.ToArray(),
                IsAuthenticated = true,
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                Message = "Token refreshed successfully"
            };
        }

        public async Task<bool> RevokeToken(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return false;

                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = null;
                var result = await _userManager.UpdateAsync(user);
                return result.Succeeded;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error revoking token for user {UserId}", userId);
                return false;
            }
        }

        public async Task<bool> Logout(string userId)
        {
            // Revoke refresh token when logging out
            return await RevokeToken(userId);
        }
    }
} 