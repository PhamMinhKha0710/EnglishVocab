using EnglishVocab.Application.Features.DifficultyLevels.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EnglishVocab.Application.Features.DifficultyLevels.Commands
{
    public class UpdateDifficultyLevelCommand : IRequest<DifficultyLevelDto>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(1, 10)]
        public int Value { get; set; }
    }
} 