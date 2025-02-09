using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Specifications.TestSpecifications;
using MindSpace.Domain.Entities.Tests;
using MindSpace.Domain.Interfaces.Repos;

namespace MindSpace.Application.Features.Tests.Queries.GetTests
{
    public class GetTestsQueryHandler : IRequestHandler<GetTestsQuery, PagedResultDTO<TestResponseDTO>>
    {
        // ================================
        // === Fields & Props
        // ================================

        private readonly ILogger<GetTestsQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // ================================
        // === Constructors
        // ================================

        public GetTestsQueryHandler(
            ILogger<GetTestsQueryHandler> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        // ================================
        // === Methods
        // ================================

        public async Task<PagedResultDTO<TestResponseDTO>> Handle(GetTestsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get list of Tests with Spec: {@Spec}", request.SpecParams);

            var spec = new TestSpecification(request.SpecParams);

            // Use Projection map to DTO
            var listDto = await _unitOfWork.Repository<Test>().GetAllWithSpecProjectedAsync<TestResponseDTO>(spec, _mapper.ConfigurationProvider);

            var count = await _unitOfWork
                 .Repository<Test>()
                 .CountAsync(spec);

            return new PagedResultDTO<TestResponseDTO>(count, listDto);
        }
    }
}