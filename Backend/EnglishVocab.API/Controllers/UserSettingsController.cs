using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EnglishVocab.API.Controllers
{
    [ApiController]
    [Route("api/user/settings")]
    [Authorize]
    public class UserSettingsController : ControllerBase
    {
        private readonly ILogger<UserSettingsController> _logger;

        public UserSettingsController(ILogger<UserSettingsController> logger)
        {
            _logger = logger;
        }

        // GET api/user/settings
        [HttpGet]
        public ActionResult GetUserSettings()
        {
            var userId = User.FindFirstValue("uid");
            _logger.LogInformation("Lấy cài đặt của user {UserId}", userId);
            
            // Mock data - Trong thực tế sẽ lấy từ database
            var settings = new
            {
                language = "Vietnamese",
                notifications = true,
                darkMode = false,
                soundEnabled = true,
                studyReminders = true,
                reminderTime = "19:00",
                studyGoalWords = 20
            };
            
            return Ok(settings);
        }

        // PUT api/user/settings
        [HttpPut]
        public ActionResult UpdateUserSettings([FromBody] UserSettingsRequest request)
        {
            var userId = User.FindFirstValue("uid");
            _logger.LogInformation("Cập nhật cài đặt cho user {UserId}", userId);
            
            // Trong thực tế sẽ lưu vào database
            return Ok(new 
            { 
                updated = true,
                settings = request
            });
        }

        // GET api/user/filters
        [HttpGet("filters")]
        public ActionResult GetFilters()
        {
            _logger.LogInformation("Lấy bộ lọc mặc định");
            
            var filters = new
            {
                difficulty = new[] { "Tất cả", "Cơ bản", "Trung bình", "Nâng cao" },
                frequency = new[] { "Tất cả", "Phổ biến", "Thường dùng", "Hiếm gặp" }
            };
            
            return Ok(filters);
        }

        // POST api/user/filters/apply
        [HttpPost("filters/apply")]
        public ActionResult ApplyFilters([FromBody] FiltersRequest request)
        {
            var userId = User.FindFirstValue("uid");
            _logger.LogInformation("Áp dụng bộ lọc cho user {UserId}: {Difficulty}, {Frequency}", 
                userId, request.Difficulty, request.Frequency);
            
            // Trong thực tế sẽ lưu vào session hoặc database
            return Ok(new 
            { 
                appliedFilters = new
                {
                    difficulty = request.Difficulty,
                    frequency = request.Frequency,
                    category = request.Category
                }
            });
        }
    }

    public class UserSettingsRequest
    {
        public string Language { get; set; }
        public bool Notifications { get; set; }
        public bool DarkMode { get; set; }
        public bool SoundEnabled { get; set; }
        public bool StudyReminders { get; set; }
        public string ReminderTime { get; set; }
        public int StudyGoalWords { get; set; }
    }

    public class FiltersRequest
    {
        public string Difficulty { get; set; }
        public string Frequency { get; set; }
        public string Category { get; set; }
    }
} 
 