using MediatR;

namespace EnglishVocab.Application.Features.DifficultyLevels.Commands
{
    public class DeleteDifficultyLevelCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
} 