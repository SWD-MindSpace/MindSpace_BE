﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.DTOs.SupportingPrograms;
using MindSpace.Application.Features.SupportingPrograms.Commands;
using MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingProgramByHistory;
using MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingProgramById;
using MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingPrograms;
using MindSpace.Application.Specifications.SupportingProgramHistorySpecifications;
using MindSpace.Application.Specifications.SupportingProgramSpecifications;

namespace MindSpace.API.Controllers
{
    //[Authorize]
    [Route("api/v{version:apiVersion}/supporting-programs")]
    public class SupportingProgramsController : BaseApiController
    {
        // ====================================
        // === Props & Fields
        // ====================================

        private readonly IMediator _mediator;

        // ====================================
        // === Constructors
        // ====================================

        public SupportingProgramsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ====================================
        // === GET
        // ====================================

        /// <summary>
        /// Get Supporting Programs By Params and Support Pagination
        /// </summary>
        /// <param name="specParams"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<SupportingProgramResponseDTO>>> GetSupportingPrograms(
            [FromQuery] SupportingProgramSpecParams specParams)
        {
            var pagedResultDTO = await _mediator.Send(new GetSupportingProgramsQuery(specParams));

            return PaginationOkResult<SupportingProgramResponseDTO>(
                pagedResultDTO.Data,
                pagedResultDTO.Count,
                specParams.PageIndex,
                specParams.PageSize
            );
        }

        /// <summary>
        /// Get the supporting program at the history registration 
        /// </summary>
        /// <param name="specParams"></param>
        /// <returns></returns>
        [HttpGet("history")]
        public async Task<ActionResult<IReadOnlyList<SupportingProgramResponseDTO>>> GetSupportingProgramsHistory(
            [FromQuery] SupportingProgramHistorySpecParams specParams)
        {
            // Get from the Table Supporting Program History to track number of SP by Student Id

            var pagedResultDTO = await _mediator.Send(new GetSupportingProgramByHistoryQuery(specParams));

            return PaginationOkResult<SupportingProgramResponseDTO>(
                pagedResultDTO.Data,
                pagedResultDTO.Count,
                specParams.PageIndex,
                specParams.PageSize
            );
        }

        /// <summary>
        /// Get Supporting Program By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SupportingProgramWithStudentsResponseDTO>> GetSupportingProgramById(
            [FromRoute] int id)
        {
            var supportProgram = await _mediator.Send(new GetSupportingProgramByIdQuery(id));
            return Ok(supportProgram);
        }

        // ====================================
        // === CREATE, PATCH, DELETE
        // ====================================

        [HttpPost]
        public async Task<ActionResult<SupportingProgramResponseDTO>> CreateSupportingProgram(
            [FromBody] CreateSupportingProgramCommand newSP)
        {
            var createdSP = await _mediator.Send(newSP);
            return CreatedAtAction(nameof(GetSupportingProgramById), new { createdSP.Id }, null);
        }
    }
}