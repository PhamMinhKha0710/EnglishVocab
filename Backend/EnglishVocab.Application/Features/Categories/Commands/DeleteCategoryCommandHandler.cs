using EnglishVocab.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.Categories.Commands
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWordRepository _wordRepository;

        public DeleteCategoryCommandHandler(
            ICategoryRepository categoryRepository,
            IWordRepository wordRepository)
        {
            _categoryRepository = categoryRepository;
            _wordRepository = wordRepository;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            // Check if the category exists
            var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);
            if (category == null)
            {
                return false;
            }

            // Check if the category has any words
            var wordsInCategory = await _wordRepository.GetByCategoryAsync(request.Id, cancellationToken);
            if (wordsInCategory.Count > 0)
            {
                // Don't allow deletion if the category has words
                throw new System.Exception("Cannot delete a category that has words. Reassign words to another category first.");
            }

            // Delete the category
            await _categoryRepository.DeleteAsync(request.Id, cancellationToken);
            return true;
        }
    }
} 