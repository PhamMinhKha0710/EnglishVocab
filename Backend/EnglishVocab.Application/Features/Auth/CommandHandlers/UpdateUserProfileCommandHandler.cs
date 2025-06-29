using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.Auth.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.Auth.CommandHandlers
{
    /// <summary>
    /// Command handler xử lý việc cập nhật hồ sơ người dùng
    /// </summary>
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, UpdateUserProfileResponse>
    {
        private readonly IUserService _userService;
        private readonly ILogger<UpdateUserProfileCommandHandler> _logger;

        public UpdateUserProfileCommandHandler(
            IUserService userService,
            ILogger<UpdateUserProfileCommandHandler> logger)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<UpdateUserProfileResponse> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Đang cập nhật thông tin hồ sơ cho người dùng có ID: {UserId}", request.UserId);
                
                // Kiểm tra người dùng tồn tại
                var existingUser = await _userService.GetUserByIdAsync(request.UserId);
                if (existingUser == null)
                {
                    _logger.LogWarning("Không tìm thấy người dùng với ID: {UserId}", request.UserId);
                    return new UpdateUserProfileResponse
                    {
                        Succeeded = false,
                        Message = "Không tìm thấy người dùng"
                    };
                }
                
                // Kiểm tra email đã tồn tại (nếu thay đổi)
                if (existingUser.Email != request.Email)
                {
                    var userWithSameEmail = await _userService.GetUserByEmailAsync(request.Email);
                    if (userWithSameEmail != null && userWithSameEmail.Id != request.UserId)
                    {
                        _logger.LogWarning("Email {Email} đã được sử dụng bởi người dùng khác", request.Email);
                        return new UpdateUserProfileResponse
                        {
                            Succeeded = false,
                            Message = "Email đã được sử dụng bởi người dùng khác"
                        };
                    }
                }
                
                // Kiểm tra username đã tồn tại (nếu thay đổi)
                if (existingUser.Username != request.Username)
                {
                    var userWithSameUsername = await _userService.GetUserByUsernameAsync(request.Username);
                    if (userWithSameUsername != null && userWithSameUsername.Id != request.UserId)
                    {
                        _logger.LogWarning("Username {Username} đã được sử dụng bởi người dùng khác", request.Username);
                        return new UpdateUserProfileResponse
                        {
                            Succeeded = false,
                            Message = "Tên đăng nhập đã được sử dụng bởi người dùng khác"
                        };
                    }
                }
                
                // Cập nhật thông tin người dùng
                var userToUpdate = new UserDto
                {
                    Id = request.UserId,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Username = request.Username,
                    IsActive = existingUser.IsActive,
                    Roles = existingUser.Roles
                };
                
                var updateResult = await _userService.UpdateUserAsync(userToUpdate);
                
                if (updateResult)
                {
                    // Lấy thông tin người dùng sau khi cập nhật
                    var updatedUser = await _userService.GetUserByIdAsync(request.UserId);
                    
                    _logger.LogInformation("Cập nhật hồ sơ thành công cho người dùng có ID: {UserId}", request.UserId);
                    
                    return new UpdateUserProfileResponse
                    {
                        Succeeded = true,
                        Message = "Cập nhật hồ sơ thành công",
                        User = new UserProfileDto
                        {
                            Id = updatedUser.Id,
                            FirstName = updatedUser.FirstName,
                            LastName = updatedUser.LastName,
                            Email = updatedUser.Email,
                            Username = updatedUser.Username,
                            Roles = updatedUser.Roles
                        }
                    };
                }
                else
                {
                    _logger.LogWarning("Cập nhật hồ sơ thất bại cho người dùng có ID: {UserId}", request.UserId);
                    return new UpdateUserProfileResponse
                    {
                        Succeeded = false,
                        Message = "Cập nhật hồ sơ thất bại"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi cập nhật hồ sơ người dùng có ID: {UserId}", request.UserId);
                return new UpdateUserProfileResponse
                {
                    Succeeded = false,
                    Message = $"Lỗi khi cập nhật hồ sơ: {ex.Message}"
                };
            }
        }
    }
} 