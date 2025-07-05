using EnglishVocab.Application.Common.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Common.Interfaces
{
    public interface ITokenService
    {
        Task<TokenDto> GenerateAccessTokenAsync(UserDto user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
} 