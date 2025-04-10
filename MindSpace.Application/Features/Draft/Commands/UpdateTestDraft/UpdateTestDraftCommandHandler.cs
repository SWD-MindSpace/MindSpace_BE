﻿using MediatR;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Domain.Entities.Drafts.TestPeriodics;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Draft.Commands.UpdateTestDraft;

public class UpdateTestDraftCommandHandler(ITestDraftService testDraftService) : IRequestHandler<UpdateTestDraftCommand, TestDraft>
{
    public async Task<TestDraft> Handle(UpdateTestDraftCommand request, CancellationToken cancellationToken)
    {
        var updatedTestDraft = await testDraftService.SetTestDraftAsync(request.TestDraft)
            ?? throw new NotFoundException(nameof(TestDraft), request.TestDraft.Id);

        return updatedTestDraft;
    }
}
