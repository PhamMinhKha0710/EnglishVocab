using EnglishVocab.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.Categories.Commands
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            // Check if the category exists
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category == null)
            {
                return false;
            }

            // Check if the category has any words
            if (category.Words?.Count > 0)
            {
                // Don't allow deletion if the category has words
                throw new System.Exception("Cannot delete a category that has words. Reassign words to another category first.");
            }

            // Delete the category
            return await _categoryRepository.DeleteAsync(request.Id);
        }
    }
} 