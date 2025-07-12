using EnglishVocab.Application.Features.Auth.Commands;
using EnglishVocab.Application.Features.Authentication.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;
using EnglishVocab.Application.Common.Models;

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
        /// Lấy thông tin hồ sơ của người dùng hiện tại
        /// </summary>
        /// <returns>Thông tin người dùng</returns>
        /// <response code="200">Lấy thành công</response>
        /// <response code="401">Không có quyền truy cập</response>
        [HttpGet("me")]
        public async Task<ActionResult<UserDto>> GetMyProfile()
        {
            // Lấy ID người dùng hiện tại từ token sử dụng claim "uid"
            var currentUserId = User.FindFirstValue("uid");
            if (string.IsNullOrEmpty(currentUserId))
            {
                _logger.LogWarning("Không thể xác định ID người dùng từ token");
                return Unauthorized(new { message = "Không thể xác định người dùng" });
            }

            _logger.LogInformation("Đang lấy thông tin hồ sơ cá nhân cho người dùng {UserId}", currentUserId);
            var query = new GetUserDetailQuery(currentUserId);
            var result = await _mediator.Send(query);

            if (result != null)
            {
                _logger.LogInformation("Lấy thông tin hồ sơ cá nhân thành công cho người dùng {UserId}", currentUserId);
                return Ok(result);
            }
            else
            {
                _logger.LogWarning("Không tìm thấy thông tin hồ sơ cho người dùng {UserId}", currentUserId);
                return NotFound(new { message = "Không tìm thấy thông tin người dùng" });
            }
        }

        /// <summary>
        /// Lấy thông tin hồ sơ của một người dùng cụ thể (chỉ dành cho Admin)
        /// </summary>
        /// <param name="userId">ID của người dùng cần lấy thông tin</param>
        /// <returns>Thông tin người dùng</returns>
        /// <response code="200">Lấy thành công</response>
        /// <response code="401">Không có quyền truy cập</response>
        /// <response code="403">Không phải là Admin</response>
        /// <response code="404">Không tìm thấy người dùng</response>
        [HttpGet("{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> GetUserDetail(string userId)
        {
            _logger.LogInformation("Admin đang lấy thông tin user có id: {UserId}", userId);
            var query = new GetUserDetailQuery(userId);
            var result = await _mediator.Send(query);
            
            if (result != null)
            {
                _logger.LogInformation("Lấy thông tin thành công cho userId: {UserId}", userId);
                return Ok(result);
            }
            else
            {
                _logger.LogWarning("Không tìm thấy user với id: {UserId}", userId);
                return NotFound(new { message = "Không tìm thấy thông tin người dùng" });
            }
        }

        /// <summary>
        /// Cập nhật hồ sơ người dùng
        /// </summary>
        /// <param name="command">Thông tin hồ sơ cần cập nhật</param>
        /// <param name="userId">ID của người dùng cần cập nhật (chỉ dành cho Admin)</param>
        /// <returns>Kết quả cập nhật và thông tin người dùng sau khi cập nhật</returns>
        /// <response code="200">Cập nhật thành công</response>
        /// <response code="400">Thông tin không hợp lệ</response>
        /// <response code="401">Không có quyền truy cập</response>
        /// <response code="403">Không có quyền cập nhật hồ sơ của người dùng khác</response>
        /// <response code="404">Không tìm thấy người dùng</response>
        [HttpPut("{userId?}")]
        public async Task<ActionResult<UpdateUserProfileResponse>> UpdateProfile(UpdateUserProfileCommand command, string userId = null)
        {
            // Lấy ID người dùng hiện tại từ token sử dụng claim "uid"
            var currentUserId = User.FindFirstValue("uid");
            if (string.IsNullOrEmpty(currentUserId))
            {
                _logger.LogWarning("Không thể xác định ID người dùng từ token");
                return Unauthorized(new { message = "Không thể xác định người dùng" });
            }

            // Xác định userId cần cập nhật
            var targetUserId = userId ?? currentUserId;
            command.UserId = targetUserId;

            // Kiểm tra quyền: nếu không phải người dùng hiện tại và không phải admin
            if (targetUserId != currentUserId && !User.IsInRole("Admin"))
            {
                _logger.LogWarning("Người dùng {CurrentUserId} đang cố cập nhật hồ sơ của người dùng {TargetUserId} nhưng không có quyền", 
                    currentUserId, targetUserId);
                return Forbid();
            }

            _logger.LogInformation("Đang xử lý yêu cầu cập nhật hồ sơ cho người dùng {UserId}", targetUserId);
            var result = await _mediator.Send(command);

            if (result.Succeeded)
            {
                _logger.LogInformation("Cập nhật hồ sơ thành công cho người dùng {UserId}", targetUserId);
                return Ok(result);
            }
            else
            {
                _logger.LogWarning("Cập nhật hồ sơ thất bại cho người dùng {UserId}: {Message}", targetUserId, result.Message);
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
            if (string.IsNullOrEmpty(currentUserId))
            {
                _logger.LogWarning("Không thể xác định ID người dùng từ token");
                return Unauthorized(new { message = "Không thể xác định người dùng" });
            }

            // Gán ID người dùng hiện tại vào command
            command.UserId = currentUserId;

            _logger.LogInformation("Đang xử lý yêu cầu cập nhật hồ sơ cá nhân cho người dùng {UserId}", currentUserId);
            var result = await _mediator.Send(command);

            if (result.Succeeded)
            {
                _logger.LogInformation("Cập nhật hồ sơ cá nhân thành công cho người dùng {UserId}", currentUserId);
                return Ok(result);
            }
            else
            {
                _logger.LogWarning("Cập nhật hồ sơ cá nhân thất bại cho người dùng {UserId}: {Message}", currentUserId, result.Message);
                return BadRequest(result);
            }
        }
    }
} 