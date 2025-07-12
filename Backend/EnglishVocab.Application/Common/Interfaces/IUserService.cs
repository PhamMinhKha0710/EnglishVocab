using EnglishVocab.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Common.Interfaces
{
    public interface IUserService
    {
        // User information properties
        string UserId { get; }
        string UserName { get; }
        bool IsAuthenticated { get; }
        
        // User retrieval methods
        Task<UserDto> GetUserByIdAsync(string userId);
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<UserDto> GetUserByUsernameAsync(string username);
        Task<UserDto> GetCurrentUserAsync();
        
        // User management methods
        Task<bool> UpdateUserAsync(UserDto userDto);
        Task<IEnumerable<string>> GetUserRolesAsync(string userId);
        Task<bool> IsInRoleAsync(string userId, string role);
        
        // Token management
        Task<bool> UpdateUserRefreshTokenAsync(string userId, string refreshToken, DateTime expiryTime);
    }
} 