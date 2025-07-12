using AutoMapper;
using EnglishVocab.Application.Features.VocabularyManagement.WordSets.DTOs;
using EnglishVocab.Domain.Entities;

namespace EnglishVocab.Application.Common.Mappings
{
    public class WordSetMappingProfile : Profile
    {
        public WordSetMappingProfile()
        {
            // WordSet mappings
            CreateMap<WordSet, WordSetDto>()
                .ForMember(dest => dest.TotalWords, opt => opt.MapFrom(src => src.WordCount))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByUserId))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.DateCreated));
        }
    }
} 