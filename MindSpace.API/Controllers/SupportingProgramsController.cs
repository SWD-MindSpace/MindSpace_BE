﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.RequestHelpers;
using MindSpace.Application.DTOs.SupportingPrograms;
using MindSpace.Application.Features.SupportingPrograms.Commands.CreateSupportingProgram;
using MindSpace.Application.Features.SupportingPrograms.Commands.PatchSupportingProgram;
using MindSpace.Application.Features.SupportingPrograms.Commands.RegisterSupportingProgram;
using MindSpace.Application.Features.SupportingPrograms.Commands.ToggleSupportingProgramStatus;
using MindSpace.Application.Features.SupportingPrograms.Commands.UnregisterSupportingProgram;
using MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingProgramByHistory;
using MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingProgramById;
using MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingPrograms;
using MindSpace.Application.Features.Tests.Commands.ToggleTestStatus;
using MindSpace.Application.Specifications.SupportingProgramHistorySpecifications;
using MindSpace.Application.Specifications.SupportingProgramSpecifications;
using MindSpace.Domain.Entities.Constants;

namespace MindSpace.API.Controllers;

//[Authorize]
[Route("api/v{version:apiVersion}/supporting-programs")]
public class SupportingProgramsController(IMediator mediator) : BaseApiController
{
    // ====================================
    // === GET
    // ====================================

    // GET /api/supporting-programs
    [Cache(30000)]
    //[HttpGet]
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

    // GET /api/supporting-programs/history?studentId=2
    [Cache(30000)]
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

    // GET /api/supporting-programs/{id}
    [Cache(30000)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<SupportingProgramSingleResponseDTO>> GetSupportingProgramById(
        [FromRoute] int id)
    {
        var supportProgram = await mediator.Send(new GetSupportingProgramByIdQuery(id));
        return Ok(supportProgram);
    }

    // ====================================
    // === CREATE, PATCH, DELETE, PUT
    // ====================================

    // POST /api/supporting-programs
    [InvalidateCache("/api/supporting-programs|")]
    [HttpPost]
    public async Task<ActionResult> CreateSupportingProgram(
        [FromBody] CreateSupportingProgramCommand newSP)
    {
        var createdSP = await mediator.Send(newSP);
        return CreatedAtAction(nameof(GetSupportingProgramById), new { createdSP.Id }, null);
    }

    // PATCH /api/supporting-programs/{id}
    [InvalidateCache("/api/supporting-programs|")]
    [HttpPatch("{id:int}")]
    public async Task<ActionResult> PatchSupportingProgram(
       [FromRoute] int id,
       [FromBody] PatchSupportingProgramCommand updatedSP)
    {
        updatedSP.Id = id;
        await mediator.Send(updatedSP);
        return NoContent();
    }

    // POST /api/supporting-programs/register
    [InvalidateCache("/api/supporting-programs/history|")]
    [HttpPost("register")]
    public async Task<ActionResult> RegisterSupportingProgram(
        [FromBody] RegisterSupportingProgramCommand registerSP)
    {
        await mediator.Send(registerSP);
        return NoContent();
    }

    // POST /api/supporting-programs/unregister
    [InvalidateCache("/api/supporting-programs/history|")]
    [HttpPost("unregister")]
    public async Task<ActionResult> UnregisterSupportingProgram(
        [FromBody] UnregisterSupportingProgramCommand unregisterSP)
    {
        await mediator.Send(unregisterSP);
        return NoContent();
    }

    // PUT /api/supporting-programs/toggle-status
    [InvalidateCache("/api/supporting-programs|")]
    [HttpPut("{id}/toggle-status")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.SchoolManager}")]
    public async Task<IActionResult> ToggleSupportingProgramStatus([FromRoute] int id)
    {
        await mediator.Send(new ToggleSupportingProgramStatusCommand { Id = id });
        return NoContent();
    }
}