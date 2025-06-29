using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EnglishVocab.Application.Features.Auth.Commands
{
    public class RevokeTokenCommand : IRequest<bool>
    {
        [Required]
        public int UserId { get; set; }
    }
} 