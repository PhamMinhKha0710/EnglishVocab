using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.Authentication.Commands;
using EnglishVocab.Application.Features.Authentication.Queries;
using EnglishVocab.Application.Features.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EnglishVocab.API.Controllers
{
    /// <summary>
    /// Controller xử lý các yêu cầu xác thực người dùng
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IMediator mediator, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Đăng ký tài khoản mới
        /// </summary>
        /// <param name="command">Thông tin đăng ký</param>
        /// <returns>Kết quả đăng ký và thông tin người dùng</returns>
        /// <response code="200">Đăng ký thành công</response>
        /// <response code="400">Thông tin đăng ký không hợp lệ</response>
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(RegisterCommand request)
        {
            _logger.LogInformation("Yêu cầu đăng ký mới từ email: {Email}", request.Email);
            var result = await _mediator.Send(request);
            
            if (result.IsAuthenticated)
            {
                _logger.LogInformation("Đăng ký thành công: {Username}", result.Username);
                return Ok(result);
            }
            else
            {
                _logger.LogWarning("Đăng ký thất bại: {Message}", result.Message);
                return BadRequest(result);
            }
        }
            
        /// <summary>
        /// Đăng nhập vào hệ thống
        /// </summary>
        /// <param name="command">Thông tin đăng nhập</param>
        /// <returns>Kết quả đăng nhập và JWT token</returns>
        /// <response code="200">Đăng nhập thành công</response>
        /// <response code="401">Thông tin đăng nhập không chính xác</response>
        /// <response code="400">Dữ liệu không hợp lệ</response>
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginCommand request)
        {
            _logger.LogInformation("Yêu cầu đăng nhập từ email: {Email}", request.Email);
            var result = await _mediator.Send(request);
            
            if (result.IsAuthenticated)
            {
                _logger.LogInformation("Đăng nhập thành công: {Username}", result.Username);
                return Ok(result);
            }
            else
            {
                _logger.LogWarning("Đăng nhập thất bại cho email {Email}: {Message}", request.Email, result.Message);
                return Unauthorized(result);
            }
        }
            
        /// <summary>
        /// Làm mới token
        /// </summary>
        /// <param name="command">Access token cũ và refresh token</param>
        /// <returns>Token mới</returns>
        /// <response code="200">Làm mới token thành công</response>
        /// <response code="401">Token không hợp lệ hoặc hết hạn</response>
        /// <response code="400">Dữ liệu không hợp lệ</response>
        [HttpPost("refresh")]
        public async Task<ActionResult<AuthResponse>> RefreshToken(RefreshTokenCommand request)
        {
            _logger.LogInformation("Yêu cầu làm mới token");
            var result = await _mediator.Send(request);
            
            if (result.IsAuthenticated)
            {
                _logger.LogInformation("Làm mới token thành công cho user: {Username}", result.Username);
                return Ok(result);
            }
            else
            {
                _logger.LogWarning("Làm mới token thất bại: {Message}", result.Message);
                return Unauthorized(result);
            }
        }

        [HttpPost("revoke")]
        [Authorize]
        public async Task<IActionResult> RevokeToken(RevokeTokenCommand request)
        {
            _logger.LogInformation("Yêu cầu thu hồi token cho userId: {UserId}", request.UserId);
            var result = await _mediator.Send(request);
            
            if (result)
            {
                _logger.LogInformation("Thu hồi token thành công cho userId: {UserId}", request.UserId);
                return Ok(new { message = "Token revoked" });
            }
            else
            {
                _logger.LogWarning("Thu hồi token thất bại cho userId: {UserId}", request.UserId);
                return BadRequest(new { message = "Token revocation failed" });
            }
        }

        /// <summary>
        /// Đăng xuất khỏi hệ thống
        /// </summary>
        /// <returns>Kết quả đăng xuất</returns>
        /// <response code="200">Đăng xuất thành công</response>
        /// <response code="400">Đăng xuất thất bại</response>
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            // Lấy userId từ claim của token
            var userId = User.FindFirst("uid")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("Không thể lấy userId từ token");
                return BadRequest(new { message = "Invalid user ID" });
            }

            _logger.LogInformation("Yêu cầu đăng xuất từ userId: {UserId}", userId);
            
            var command = new RevokeTokenCommand { UserId = userId };
            var result = await _mediator.Send(command);
            
            if (result)
            {
                _logger.LogInformation("Đăng xuất thành công cho userId: {UserId}", userId);
                return Ok(new { message = "Logout successful" });
            }
            else
            {
                _logger.LogWarning("Đăng xuất thất bại cho userId: {UserId}", userId);
                return BadRequest(new { message = "Logout failed" });
            }
        }
    }
} 