﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities.Drafts.TestPeriodic
{
    public class TestDraft
    {
        public required string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TestCategoryId { get; set; }
        public int AuthorId { get; set; }
        public int SpecializationId { get; set; }
        public int QuestionCount { get; set; }
        public decimal Price { get; set; }
        public List<QuestionDraft> QuestionItems { get; set; } = new List<QuestionDraft>();
    }
}
