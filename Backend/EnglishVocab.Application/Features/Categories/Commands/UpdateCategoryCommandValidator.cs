using FluentValidation;

namespace EnglishVocab.Application.Features.Categories.Commands
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Category ID is required.")
                .GreaterThan(0).WithMessage("Category ID must be greater than 0.");
                
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(50).WithMessage("Category name must not exceed 50 characters.");

            RuleFor(c => c.Description)
                .MaximumLength(200).WithMessage("Description must not exceed 200 characters.");
        }
    }
} 