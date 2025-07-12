using AutoMapper;
using EnglishVocab.Application.Features.StudyManagement.DTOs;
using EnglishVocab.Domain.Entities;

namespace EnglishVocab.Application.Common.Mappings
{
    public class UserProgressMappingProfile : Profile
    {
        public UserProgressMappingProfile()
        {
            // UserProgress mapping
            CreateMap<UserProgress, SessionStatisticsDto>()
                .ForMember(dest => dest.SessionId, opt => opt.Ignore())
                .ForMember(dest => dest.Time, opt => opt.Ignore())
                .ForMember(dest => dest.Known, opt => opt.Ignore())
                .ForMember(dest => dest.NeedReview, opt => opt.Ignore())
                .ForMember(dest => dest.Progress, opt => opt.Ignore())
                .ForMember(dest => dest.TotalWords, opt => opt.Ignore())
                .ForMember(dest => dest.CompletedWords, opt => opt.Ignore());
        }
    }
} 