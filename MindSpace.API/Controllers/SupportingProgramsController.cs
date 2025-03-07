﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.DTOs.SupportingPrograms;
using MindSpace.Application.Features.SupportingPrograms.Commands.CreateSupportingProgram;
using MindSpace.Application.Features.SupportingPrograms.Commands.PatchSupportingProgram;
using MindSpace.Application.Features.SupportingPrograms.Commands.RegisterSupportingProgram;
using MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingProgramByHistory;
using MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingProgramById;
using MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingPrograms;
using MindSpace.Application.Specifications.SupportingProgramHistorySpecifications;
using MindSpace.Application.Specifications.SupportingProgramSpecifications;

namespace MindSpace.API.Controllers;

//[Authorize]
[Route("api/v{version:apiVersion}/supporting-programs")]
public class SupportingProgramsController(IMediator mediator) : BaseApiController
{
    // ====================================
    // === GET
    // ====================================

    // GET: /supporting-programs/
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<SupportingProgramResponseDTO>>> GetSupportingPrograms(
        [FromQuery] SupportingProgramSpecParams specParams)
    {
        var pagedResultDTO = await mediator.Send(new GetSupportingProgramsQuery(specParams));

        return PaginationOkResult<SupportingProgramResponseDTO>(
            pagedResultDTO.Data,
            pagedResultDTO.Count,
            specParams.PageIndex,
            specParams.PageSize
        );
    }

    // GET: /supporting-programs/history?studentId=2
    [HttpGet("history")]
    public async Task<ActionResult<IReadOnlyList<SupportingProgramResponseDTO>>> GetSupportingProgramsHistory(
        [FromQuery] SupportingProgramHistorySpecParams specParams)
    {
        // Get from the Table Supporting Program History to track number of SP by Student Id

        var pagedResultDTO = await mediator.Send(new GetSupportingProgramByHistoryQuery(specParams));

        return PaginationOkResult<SupportingProgramResponseDTO>(
            pagedResultDTO.Data,
            pagedResultDTO.Count,
            specParams.PageIndex,
            specParams.PageSize
        );
    }

    // GET: /supporting-programs/2
    [HttpGet("{id:int}")]
    public async Task<ActionResult<SupportingProgramWithStudentsResponseDTO>> GetSupportingProgramById(
        [FromRoute] int id)
    {
        var supportProgram = await mediator.Send(new GetSupportingProgramByIdQuery(id));
        return Ok(supportProgram);
    }

    // ====================================
    // === CREATE, PATCH, DELETE
    // ====================================

    // POST: /supporting-programs
    [HttpPost]
    public async Task<ActionResult> CreateSupportingProgram(
        [FromBody] CreateSupportingProgramCommand newSP)
    {
        var createdSP = await mediator.Send(newSP);
        return CreatedAtAction(nameof(GetSupportingProgramById), new { createdSP.Id }, null);
    }

    // PATCH: /supporting-programs/2
    [HttpPatch("{id:int}")]
    public async Task<ActionResult> PatchSupportingProgram(
        [FromRoute] int id,
        [FromBody] PatchSupportingProgramCommand updatedSP)
    {
        updatedSP.Id = id;
        await mediator.Send(updatedSP);
        return NoContent();
    }

    // POST: /supporting-programs/ 
    [HttpPost("register")]
    public async Task<ActionResult> RegisterSupportingProgram(
        [FromBody] RegisterSupportingProgramCommand registerSP)
    {
        await mediator.Send(registerSP);
        return NoContent();
    }
}