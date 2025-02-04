﻿using MindSpace.Application.Specifications;
using MindSpace.Domain.Entities.SupportingPrograms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.SupportingPrograms.Specs
{
    public class SupportingProgramSpecification : BaseSpecification<SupportingProgram>
    {
        /// <summary>
        /// Filter Supporting Program with params
        /// </summary>
        /// <param name="spParams"></param>
        public SupportingProgramSpecification(SupportingProgramSpecParams spParams)
            : base(x =>
                (!spParams.MinQuantity.HasValue || x.MaxQuantity >= spParams.MinQuantity) &&
                (!spParams.MaxQuantity.HasValue || x.MaxQuantity <= spParams.MaxQuantity))
        {

            ApplyPaging(spParams.PageSize * (spParams.PageIndex - 1), spParams.PageSize);

            if (!string.IsNullOrEmpty(spParams.Sort))
            {
                switch (spParams.Sort)
                {
                    case "managerIdAsc":
                        AddOrderBy(x => x.ManagerId.ToString()); break;
                    case "managerIdDesc":
                        AddOrderBy(x => x.ManagerId.ToString()); break;
                    default:
                        AddOrderBy(x => x.Id.ToString()); break;
                }
            }
        }
    }
}
