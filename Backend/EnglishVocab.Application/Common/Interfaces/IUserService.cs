using EnglishVocab.Application.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(int userId);
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<UserDto> GetUserByUsernameAsync(string username);
        Task<bool> UpdateUserAsync(UserDto userDto);
        Task<IEnumerable<string>> GetUserRolesAsync(int userId);
        Task<bool> IsInRoleAsync(int userId, string role);
        Task<bool> UpdateUserRefreshTokenAsync(int userId, string refreshToken, System.DateTime expiryTime);
        string UserId { get; }
        string UserName { get; }
    }
} 