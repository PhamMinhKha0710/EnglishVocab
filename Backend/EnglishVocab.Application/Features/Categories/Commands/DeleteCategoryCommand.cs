using MediatR;

namespace EnglishVocab.Application.Features.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
} 