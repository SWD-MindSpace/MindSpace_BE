﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.RequestHelpers;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Features.TestResponses.Queries.GetTestScoreRankByTotalScore;

namespace MindSpace.API.Controllers;

[Route("api/v{version:apiVersion}/test-score-ranks")]
public class TestScoreRanksController(IMediator mediator) : BaseApiController
{
    // ====================================
    // === GET
    // ====================================

    // GET /api/v1/test-score-ranks?totalScore={totalScore}&testId={testId}
    // Get the rank of a test score based on total score and test ID
    [Cache(300)]
    [HttpGet]
    public async Task<ActionResult<TestScoreRankResponseDTO>> GetTestScoreRankByTotalScore([FromQuery] int totalScore, int testId)
    {
        var data = await mediator.Send(new GetTestScoreRankByTotalScoreQuery(totalScore, testId));
        return Ok(data);
    }
}
