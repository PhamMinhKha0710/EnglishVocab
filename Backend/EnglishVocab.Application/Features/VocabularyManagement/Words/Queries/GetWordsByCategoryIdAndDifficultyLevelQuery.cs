using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Queries
{
    public class GetWordsByCategoryIdAndDifficultyLevelQuery : IRequest<IEnumerable<WordDto>>
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string DifficultyLevel { get; set; }
    }
} 