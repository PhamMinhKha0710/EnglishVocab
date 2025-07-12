using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Commands
{
    public class CreateWordCommand : IRequest<WordDto>
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string English { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Vietnamese { get; set; }

        [StringLength(100)]
        public string Pronunciation { get; set; }

        [StringLength(500)]
        public string Example { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; }

        [StringLength(255)]
        public string ImageUrl { get; set; }

        [StringLength(255)]
        public string AudioUrl { get; set; }

        [Required]
        public string DifficultyLevel { get; set; }

        public int? CategoryId { get; set; }
    }
} 
 