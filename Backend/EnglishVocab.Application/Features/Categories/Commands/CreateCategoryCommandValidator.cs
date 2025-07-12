using FluentValidation;

namespace EnglishVocab.Application.Features.Categories.Commands
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(50).WithMessage("Category name must not exceed 50 characters.");

            RuleFor(c => c.Description)
                .MaximumLength(200).WithMessage("Description must not exceed 200 characters.");
        }
    }
} 