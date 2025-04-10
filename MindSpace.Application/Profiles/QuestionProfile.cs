﻿using AutoMapper;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Domain.Entities.Drafts.TestPeriodics;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateProjection<Question, QuestionResponseDTO>()
                .ForMember(d => d.Content, a => a.MapFrom(q => q.Content != null ? q.Content : null))
                .ForMember(d => d.QuestionOptions, a => a.MapFrom(q => q.QuestionOptions.Select(opt => opt)));

            CreateMap<QuestionDraft, Question>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Content, a => a.MapFrom(q => q.Content))
                .ForMember(d => d.QuestionOptions, a => a.MapFrom(q => q.QuestionOptions.Select(opt => opt)));
        }
    }
}