using System.Threading.Tasks;
using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.Authentication.Commands;

namespace EnglishVocab.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(LoginCommand request);
        Task<AuthResponse> Register(RegisterCommand request);
        Task<AuthResponse> RefreshToken(RefreshTokenCommand request);
        Task<TokenDto> RefreshTokenAsync(string accessToken, string refreshToken);
        Task<bool> RevokeToken(string userId);
        Task<bool> Logout(string userId);
    }

    public class AuthResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] Roles { get; set; }
        public bool IsAuthenticated { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Message { get; set; }
    }
} 