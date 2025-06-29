using EnglishVocab.Application.Common.Models;
using MediatR;

namespace EnglishVocab.Application.Features.Auth.Queries
{
    public class GetUserDetailQuery : IRequest<UserDto>
    {
        public int UserId { get; set; }

        public GetUserDetailQuery(int userId)
        {
            UserId = userId;
        }
    }
} 