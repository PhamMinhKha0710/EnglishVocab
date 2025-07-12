using FluentValidation;
using System;
using EnglishVocab.Constants.Constant;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Queries
{
    public class GetWordsByCategoryIdAndDifficultyLevelQueryValidator : AbstractValidator<GetWordsByCategoryIdAndDifficultyLevelQuery>
    {
        public GetWordsByCategoryIdAndDifficultyLevelQueryValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId is required.");

            RuleFor(x => x.DifficultyLevel)
                .NotEmpty().WithMessage("DifficultyLevel is required.")
                .Must(BeValidDifficultyLevel).WithMessage("Invalid DifficultyLevel value.");
        }

        private bool BeValidDifficultyLevel(string difficultyLevel)
        {
            return Enum.TryParse<DifficultyLevelType>(difficultyLevel, out _);
        }
    }
} 