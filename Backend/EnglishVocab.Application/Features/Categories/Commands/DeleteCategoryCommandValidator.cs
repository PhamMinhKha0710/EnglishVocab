using FluentValidation;

namespace EnglishVocab.Application.Features.Categories.Commands
{
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Category ID is required.")
                .GreaterThan(0).WithMessage("Category ID must be greater than 0.");
        }
    }
} 