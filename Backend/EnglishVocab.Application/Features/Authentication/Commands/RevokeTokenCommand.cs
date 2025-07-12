using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EnglishVocab.Application.Features.Auth.Commands
{
    public class RevokeTokenCommand : IRequest<bool>
    {
        public string UserId { get; set; }
    }
} 