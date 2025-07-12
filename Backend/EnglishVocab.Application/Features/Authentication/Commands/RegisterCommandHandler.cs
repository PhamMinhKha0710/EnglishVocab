using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.Authentication.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.Authentication.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
    {
        private readonly IAuthService _authService;
        private readonly ILogger<RegisterCommandHandler> _logger;

        public RegisterCommandHandler(
            IAuthService authService,
            ILogger<RegisterCommandHandler> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _authService.Register(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering user");
                return new AuthResponse
                {
                    IsAuthenticated = false,
                    Message = "Đã xảy ra lỗi trong quá trình đăng ký: " + ex.Message
                };
            }
        }
    }
} 