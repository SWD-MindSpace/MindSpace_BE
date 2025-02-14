﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.DTOs.Tests
{
    public class TestOverviewResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TestCode { get; set; }
        public string? TestCategoryId { get; set; } // may change to TestCategoryResponseDTO later if needed
        public string? SpecializationId { get; set; } // may change to SpecializationResponseDTO later if needed
        public string TargetUser { get; set; }
        public string? Description { get; set; }
        public int QuestionCount { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
    }
}
