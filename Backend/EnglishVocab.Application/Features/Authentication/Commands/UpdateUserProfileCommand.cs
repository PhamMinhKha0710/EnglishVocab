using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EnglishVocab.Application.Features.Auth.Commands
{
    /// <summary>
    /// Command để cập nhật thông tin hồ sơ người dùng
    /// </summary>
    public class UpdateUserProfileCommand : IRequest<UpdateUserProfileResponse>
    {
        /// <summary>
        /// ID của người dùng cần cập nhật
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Tên người dùng
        /// </summary>
        [Required(ErrorMessage = "Tên người dùng là bắt buộc")]
        [StringLength(50, ErrorMessage = "Tên người dùng không được vượt quá 50 ký tự")]
        public string FirstName { get; set; }
        
        /// <summary>
        /// Họ người dùng
        /// </summary>
        [Required(ErrorMessage = "Họ người dùng là bắt buộc")]
        [StringLength(50, ErrorMessage = "Họ người dùng không được vượt quá 50 ký tự")]
        public string LastName { get; set; }
        
        /// <summary>
        /// Email người dùng
        /// </summary>
        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự")]
        public string Email { get; set; }
        
        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        [StringLength(50, ErrorMessage = "Tên đăng nhập không được vượt quá 50 ký tự")]
        public string Username { get; set; }
    }

    /// <summary>
    /// Kết quả trả về khi cập nhật hồ sơ người dùng
    /// </summary>
    public class UpdateUserProfileResponse
    {
        /// <summary>
        /// Trạng thái thành công
        /// </summary>
        public bool Succeeded { get; set; }
        
        /// <summary>
        /// Thông báo kết quả
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Thông tin người dùng sau khi cập nhật
        /// </summary>
        public UserProfileDto User { get; set; }
    }

    /// <summary>
    /// DTO chứa thông tin hồ sơ người dùng
    /// </summary>
    public class UserProfileDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] Roles { get; set; }
        
        public string FullName => $"{FirstName} {LastName}".Trim();
    }
} 