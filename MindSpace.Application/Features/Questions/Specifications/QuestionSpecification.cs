﻿using MindSpace.Application.Specifications;
using MindSpace.Domain.Entities.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.Questions.Specifications
{
    public class QuestionSpecification : BaseSpecification<Question>
    {
        public QuestionSpecification(QuestionSpecParams specParams)
            : base(q =>
                string.IsNullOrEmpty(specParams.SearchQuestionContent) || q.Content.ToLower().Contains(specParams.SearchQuestionContent.ToLower()))
        {
            // Add include
            AddInclude(x => x.QuestionOptions);

            // Add Paging
            AddPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            // Add Sorting
            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "content":
                        AddOrderBy(x => x.Content); break;
                    case "createAt":
                        AddOrderByDescending(x => x.CreateAt.ToString()); break;
                    default:
                        AddOrderBy(x => x.Id.ToString()); break;
                }
            }
        }
    }
}
