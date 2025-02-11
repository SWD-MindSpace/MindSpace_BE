﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.SupportingPrograms;
using MindSpace.Application.Specifications.SupportingProgramSpecifications;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.SupportingPrograms.Commands.CreateSupportingProgram
{
    public class CreateSupportingProgramCommandHandler
        : IRequestHandler<CreateSupportingProgramCommand, SupportingProgramResponseDTO>
    {
        // ================================
        // === Fields & Props
        // ================================

        private readonly ILogger<CreateSupportingProgramCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // ================================
        // === Constructors
        // ================================

        public CreateSupportingProgramCommandHandler(ILogger<CreateSupportingProgramCommandHandler> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // ================================
        // === Methods
        // ================================

        public async Task<SupportingProgramResponseDTO> Handle(CreateSupportingProgramCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Create Supporting Program with Title: {@title}", request.Title);

            var spec = new SupportingProgramSpecification(request.Title);
            var existingProgram = await _unitOfWork.Repository<SupportingProgram>().GetBySpecAsync(spec);

            // If existed then throw exception
            if (existingProgram != null) throw new ResourceAlreadyExistsException(request.Title);

            // Update or throw exception
            var spToCreate = _mapper.Map<SupportingProgram>(request);
            var addedSP = _unitOfWork.Repository<SupportingProgram>().Insert(spToCreate)
                ?? throw new UpdateFailedException(nameof(SupportingProgram));

            await _unitOfWork.CompleteAsync();
            return _mapper.Map<SupportingProgram, SupportingProgramResponseDTO>(addedSP);
        }
    }
}
