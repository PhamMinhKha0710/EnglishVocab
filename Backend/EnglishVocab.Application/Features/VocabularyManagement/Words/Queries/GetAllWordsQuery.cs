using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using MediatR;
using System.Collections.Generic;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Queries
{
    public class GetAllWordsQuery : IRequest<IEnumerable<WordDto>>
    {
        // Không cần thuộc tính vì đây là query đơn giản để lấy tất cả từ vựng
    }
} 