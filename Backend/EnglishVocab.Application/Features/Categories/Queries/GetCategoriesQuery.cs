using EnglishVocab.Application.Features.Categories.DTOs;
using MediatR;
using System.Collections.Generic;

namespace EnglishVocab.Application.Features.Categories.Queries
{
    public class GetCategoriesQuery : IRequest<IEnumerable<CategoryDto>>
    {
    }
} 