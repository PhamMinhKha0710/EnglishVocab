using AutoMapper;
using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using EnglishVocab.Application.Features.StudyManagement.DTOs;
using EnglishVocab.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace EnglishVocab.Application.Common.Mappings
{
    public class WordMappingProfile : Profile
    {
        public WordMappingProfile()
        {
            CreateMap<Word, WordDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.Ignore())
                .ForMember(dest => dest.DifficultyLevelName, opt => opt.Ignore())
                .ForMember(dest => dest.DifficultyValue, opt => opt.Ignore());
                
            CreateMap<WordDto, Word>()
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
                .ForMember(dest => dest.DateModified, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());

            // Word to Flashcard mapping
            CreateMap<Word, EnglishVocab.Application.Features.StudyManagement.DTOs.FlashcardDto>()
                .ForMember(dest => dest.WordId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.English))
                .ForMember(dest => dest.Translation, opt => opt.MapFrom(src => src.Vietnamese))
                .ForMember(dest => dest.Examples, opt => opt.MapFrom(src => new List<string> { src.Example }))
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.DifficultyLevel, opt => opt.Ignore());
        }
    }
} 