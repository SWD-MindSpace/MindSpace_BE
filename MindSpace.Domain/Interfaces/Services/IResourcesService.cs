﻿using AutoMapper;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Resources;
using MindSpace.Domain.Interfaces.Specifications;

namespace MindSpace.Domain.Interfaces.Services
{
    public interface IResourcesService
    {
        public Task<IReadOnlyList<Resource>> GetResourcesAsync(ISpecification<Resource> resourceSpec, IConfigurationProvider mappingConfiguration);
        public Task<IReadOnlyList<Resource>> GetResourceAsync(ISpecification<Resource> resourceSpec, IConfigurationProvider mappingConfiguration);
        public Task<int> CountResourcesWithSpecAsync(ISpecification<Resource> resourceSpec);
        public ResourceType[] GetResourceTypes();

        public Task<bool> DisableResource(Resource resource);

        public Task<Resource?> CreateResourceAsArticle(Resource resource, int schoolManagerId);
        public Task<Resource?> CreateResourceAsPaper(Resource resource, int schoolManagerId);
        public Task<ResourceSection> CreateResourceSection(ResourceSection section);
    }
}
