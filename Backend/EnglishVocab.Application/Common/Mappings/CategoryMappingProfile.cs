using AutoMapper;
using EnglishVocab.Application.Features.Categories.DTOs;
using EnglishVocab.Domain.Entities;

namespace EnglishVocab.Application.Common.Mappings
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            // Category mappings - don't rely on navigation properties
            CreateMap<Category, CategoryDto>();
        }
    }
} 