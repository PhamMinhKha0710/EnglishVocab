using EnglishVocab.Application.Features.Categories.DTOs;
using MediatR;

namespace EnglishVocab.Application.Features.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<CategoryDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
} 