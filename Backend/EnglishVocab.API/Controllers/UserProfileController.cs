using EnglishVocab.Application.Features.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EnglishVocab.API.Controllers
{
    /// <summary>
    /// Controller xử lý các yêu cầu liên quan đến hồ sơ người dùng
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserProfileController> _logger;

        public UserProfileController(IMediator mediator, ILogger<UserProfileController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Cập nhật thông tin hồ sơ người dùng
        /// </summary>
        /// <param name="command">Thông tin hồ sơ cần cập nhật</param>
        /// <returns>Kết quả cập nhật và thông tin người dùng sau khi cập nhật</returns>
        /// <response code="200">Cập nhật thành công</response>
        /// <response code="400">Thông tin không hợp lệ</response>
        /// <response code="401">Không có quyền truy cập</response>
        /// <response code="403">Không có quyền cập nhật hồ sơ của người dùng khác</response>
        [HttpPut("update")]
        public async Task<ActionResult<UpdateUserProfileResponse>> UpdateProfile(UpdateUserProfileCommand command)
        {
            // Lấy ID người dùng hiện tại từ token sử dụng claim "uid"
            var currentUserId = User.FindFirstValue("uid");
            if (string.IsNullOrEmpty(currentUserId) || !int.TryParse(currentUserId, out int userId))
            {
                _logger.LogWarning("Không thể xác định ID người dùng từ token");
                return Unauthorized(new { message = "Không thể xác định người dùng" });
            }

            // Kiểm tra nếu người dùng đang cố cập nhật hồ sơ của người khác
            // Chỉ cho phép Admin cập nhật hồ sơ của người khác
            if (command.UserId != userId && !User.IsInRole("Admin"))
            {
                _logger.LogWarning("Người dùng {CurrentUserId} đang cố cập nhật hồ sơ của người dùng {TargetUserId}", userId, command.UserId);
                return Forbid();
            }

            _logger.LogInformation("Đang xử lý yêu cầu cập nhật hồ sơ cho người dùng {UserId}", command.UserId);
            var result = await _mediator.Send(command);

            if (result.Succeeded)
            {
                _logger.LogInformation("Cập nhật hồ sơ thành công cho người dùng {UserId}", command.UserId);
                return Ok(result);
            }
            else
            {
                _logger.LogWarning("Cập nhật hồ sơ thất bại cho người dùng {UserId}: {Message}", command.UserId, result.Message);
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Cập nhật hồ sơ của người dùng hiện tại
        /// </summary>
        /// <param name="command">Thông tin hồ sơ cần cập nhật</param>
        /// <returns>Kết quả cập nhật và thông tin người dùng sau khi cập nhật</returns>
        [HttpPut("me")]
        public async Task<ActionResult<UpdateUserProfileResponse>> UpdateMyProfile(UpdateUserProfileCommand command)
        {
            // Lấy ID người dùng hiện tại từ token sử dụng claim "uid"
            var currentUserId = User.FindFirstValue("uid");
            if (string.IsNullOrEmpty(currentUserId) || !int.TryParse(currentUserId, out int userId))
            {
                _logger.LogWarning("Không thể xác định ID người dùng từ token");
                return Unauthorized(new { message = "Không thể xác định người dùng" });
            }

            // Gán ID người dùng hiện tại vào command
            command.UserId = userId;

            _logger.LogInformation("Đang xử lý yêu cầu cập nhật hồ sơ cá nhân cho người dùng {UserId}", userId);
            var result = await _mediator.Send(command);

            if (result.Succeeded)
            {
                _logger.LogInformation("Cập nhật hồ sơ cá nhân thành công cho người dùng {UserId}", userId);
                return Ok(result);
            }
            else
            {
                _logger.LogWarning("Cập nhật hồ sơ cá nhân thất bại cho người dùng {UserId}: {Message}", userId, result.Message);
                return BadRequest(result);
            }
        }
    }
} 