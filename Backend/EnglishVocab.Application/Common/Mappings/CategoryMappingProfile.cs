using AutoMapper;
using EnglishVocab.Application.Features.Categories.DTOs;
using EnglishVocab.Domain.Entities;

namespace EnglishVocab.Application.Common.Mappings
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            // Category mappings
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.WordCount, opt => opt.MapFrom(src => src.Words != null ? src.Words.Count : 0));
        }
    }
} 