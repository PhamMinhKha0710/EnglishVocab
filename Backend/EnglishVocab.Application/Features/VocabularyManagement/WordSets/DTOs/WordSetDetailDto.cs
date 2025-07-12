using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using EnglishVocab.Application.Features.VocabularyManagement.WordSets.DTOs;
using System.Collections.Generic;

namespace EnglishVocab.Application.Features.VocabularyManagement.WordSets.DTOs
{
    public class WordSetDetailDto : WordSetDto
    {
        public List<WordDto> Words { get; set; } = new List<WordDto>();
    }
} 