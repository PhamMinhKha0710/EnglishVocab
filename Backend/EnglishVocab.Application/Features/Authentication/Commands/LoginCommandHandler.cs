using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.Authentication.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.Authentication.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IAuthService _authService;
        private readonly ILogger<LoginCommandHandler> _logger;

        public LoginCommandHandler(
            IAuthService authService,
            ILogger<LoginCommandHandler> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _authService.Login(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while logging in");
                return new AuthResponse
                {
                    IsAuthenticated = false,
                    Message = "Đã xảy ra lỗi trong quá trình đăng nhập: " + ex.Message
                };
            }
        }
    }
} 