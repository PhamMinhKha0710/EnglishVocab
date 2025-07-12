using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using MediatR;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Queries
{
    public class GetDataTableWordsQuery : DataTableRequest, IRequest<DataTableResponse<WordDto>>
    {
        // Thêm các thuộc tính lọc đặc biệt cho từ vựng
        public int? CategoryId { get; set; }
        public string DifficultyLevel { get; set; }
    }
} 