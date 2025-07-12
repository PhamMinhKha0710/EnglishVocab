using AutoMapper;
using EnglishVocab.Application.Features.StudyManagement.DTOs;
using EnglishVocab.Domain.Entities;
using System;

namespace EnglishVocab.Application.Common.Mappings
{
    public class StudySessionMappingProfile : Profile
    {
        public StudySessionMappingProfile()
        {
            // StudySession mappings
            CreateMap<StudySession, StudySessionDto>()
                .ForMember(dest => dest.StudyMode, opt => opt.MapFrom(src => src.StudyMode.ToString()))
                .ForMember(dest => dest.WordSetName, opt => opt.Ignore()) // This will be set manually in handler
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => 
                    src.EndTime.HasValue ? src.EndTime.Value - src.StartTime : TimeSpan.Zero));
        }
    }
} 