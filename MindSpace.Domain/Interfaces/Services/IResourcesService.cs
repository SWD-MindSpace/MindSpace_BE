﻿using AutoMapper;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Resources;
using MindSpace.Domain.Interfaces.Specifications;

namespace MindSpace.Domain.Interfaces.Services
{
    public interface IResourcesService
    {
        public Resource? CreateResourceAsArticle(Resource resource, int schoolManagerId);
        public ResourceType[] GetResourceTypes();
    }
}
