using EnglishVocab.Application.Features.Categories.DTOs;
using MediatR;

namespace EnglishVocab.Application.Features.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<CategoryDto>
    {
        public int Id { get; set; }
    }
} 