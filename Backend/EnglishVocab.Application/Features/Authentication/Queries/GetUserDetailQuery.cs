using EnglishVocab.Application.Common.Models;
using MediatR;

namespace EnglishVocab.Application.Features.Authentication.Queries
{
    public class GetUserDetailQuery : IRequest<UserDto>
    {
        public string UserId { get; set; }

        public GetUserDetailQuery(string userId)
        {
            UserId = userId;
        }
    }
} 