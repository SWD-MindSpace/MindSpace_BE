﻿using AutoMapper;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Resources;
using MindSpace.Domain.Interfaces.Repos;
using MindSpace.Domain.Interfaces.Services;
using MindSpace.Domain.Interfaces.Specifications;
using System.Linq;

namespace MindSpace.Application.Services
{
    public class ResourcesService : IResourcesService
    {
        // ===================================
        // === Fields & Prop
        // ===================================

        private readonly IUnitOfWork _unitOfWork;

        // ===================================
        // === Constructors
        // ===================================
        public ResourcesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ===================================
        // === Methods
        // ===================================

        public ResourceType[] GetResourceTypes()
        {
            return Enum
                .GetValues(typeof(ResourceType))
                .Cast<ResourceType>().ToArray();
        }

        public Resource? CreateResourceAsArticle(Resource resource, int schoolManagerId)
        {
            if (resource.ArticleUrl == null) return null;

            var addedResource = _unitOfWork.Repository<Resource>().Insert(resource);
            if (addedResource == null) return null;

            return addedResource;
        }
    }
}
