using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.Auth.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.Auth.Queries
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDto>
    {
        private readonly IUserService _userService;
        private readonly ILogger<GetUserDetailQueryHandler> _logger;

        public GetUserDetailQueryHandler(
            IUserService userService,
            ILogger<GetUserDetailQueryHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<UserDto> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _userService.GetUserByIdAsync(request.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting user details for user {UserId}", request.UserId);
                return null;
            }
        }
    }
} 