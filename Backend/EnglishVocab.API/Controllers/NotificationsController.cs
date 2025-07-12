using EnglishVocab.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EnglishVocab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<NotificationsController> _logger;

        public NotificationsController(INotificationRepository notificationRepository, ILogger<NotificationsController> logger)
        {
            _notificationRepository = notificationRepository;
            _logger = logger;
        }

        /// <summary>
        /// Lấy danh sách thông báo của người dùng
        /// </summary>
        /// <param name="unreadOnly">Nếu true, chỉ lấy thông báo chưa đọc. Mặc định là false (lấy tất cả)</param>
        /// <returns>Danh sách thông báo</returns>
        [HttpGet]
        public async Task<ActionResult> GetNotifications([FromQuery] bool unreadOnly = false)
        {
            var userId = User.FindFirstValue("uid");
            
            if (unreadOnly)
            {
                _logger.LogInformation("Lấy thông báo chưa đọc cho user {UserId}", userId);
                var notifications = await _notificationRepository.GetUnreadByUserIdAsync(userId);
                return Ok(notifications);
            }
            else
            {
                _logger.LogInformation("Lấy tất cả thông báo cho user {UserId}", userId);
                var notifications = await _notificationRepository.GetByUserIdAsync(userId);
                return Ok(notifications);
            }
        }

        // GET api/notifications/count
        [HttpGet("count")]
        public async Task<ActionResult> GetUnreadCount()
        {
            var userId = User.FindFirstValue("uid");
            _logger.LogInformation("Lấy số lượng thông báo chưa đọc cho user {UserId}", userId);
            
            var count = await _notificationRepository.CountUnreadAsync(userId);
            return Ok(new { count });
        }

        // POST api/notifications/{id}/read
        [HttpPost("{id}/read")]
        public async Task<ActionResult> MarkAsRead(int id)
        {
            var userId = User.FindFirstValue("uid");
            _logger.LogInformation("Đánh dấu đã đọc thông báo {Id} cho user {UserId}", id, userId);
            
            var result = await _notificationRepository.MarkAsReadAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(new { id, isRead = true });
        }

        // POST api/notifications/read-all
        [HttpPost("read-all")]
        public async Task<ActionResult> MarkAllAsRead()
        {
            var userId = User.FindFirstValue("uid");
            _logger.LogInformation("Đánh dấu đã đọc tất cả thông báo cho user {UserId}", userId);
            
            var result = await _notificationRepository.MarkAllAsReadAsync(userId);
            return Ok(new { success = result });
        }
    }
} 
 