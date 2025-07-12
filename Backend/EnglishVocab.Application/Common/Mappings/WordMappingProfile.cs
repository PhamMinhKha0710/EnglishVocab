using AutoMapper;
using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using EnglishVocab.Application.Features.StudyManagement.DTOs;
using EnglishVocab.Domain.Entities;
using System.Collections.Generic;

namespace EnglishVocab.Application.Common.Mappings
{
    public class WordMappingProfile : Profile
    {
        public WordMappingProfile()
        {
            // Word mappings
            CreateMap<Word, WordDto>()
                .ForMember(dest => dest.DifficultyLevel, opt => opt.MapFrom(src => src.DifficultyLevel.ToString()))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryEntity != null ? src.CategoryEntity.Name : src.Category))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));

            // Word to Flashcard mapping
            CreateMap<Word, EnglishVocab.Application.Features.StudyManagement.DTOs.FlashcardDto>()
                .ForMember(dest => dest.WordId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.English))
                .ForMember(dest => dest.Translation, opt => opt.MapFrom(src => src.Vietnamese))
                .ForMember(dest => dest.Examples, opt => opt.MapFrom(src => new List<string> { src.Example }))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.CategoryEntity != null ? src.CategoryEntity.Name : src.Category))
                .ForMember(dest => dest.DifficultyLevel, opt => opt.MapFrom(src => src.DifficultyLevel.ToString()));
        }
    }
} 