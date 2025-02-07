﻿using MediatR;
using MindSpace.Application.Features.SupportingProgramHistories.Specifications;
using MindSpace.Application.Features.SupportingPrograms.Specifications;

namespace MindSpace.Application.Features.SupportingProgramHistories.Queries.GetCountSupportingProgramHistories
{
    public class GetCountSupportingProgramHistoryQuery : IRequest<int>
    {
        public SupportingProgramHistorySpecParams SpecParams { get; private set; }

        public GetCountSupportingProgramHistoryQuery(SupportingProgramHistorySpecParams specParams)
        {
            SpecParams = specParams;
        }
    }
}
