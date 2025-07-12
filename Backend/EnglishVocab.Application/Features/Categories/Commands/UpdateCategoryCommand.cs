using EnglishVocab.Application.Features.Categories.DTOs;
using MediatR;

namespace EnglishVocab.Application.Features.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest<CategoryDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
} 