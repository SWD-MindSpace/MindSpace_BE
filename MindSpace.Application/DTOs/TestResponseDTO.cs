﻿using MindSpace.Domain.Entities.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.DTOs
{
    public class TestResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? TestCode { get; set; }
        public string TargetUser { get; set; }
        public string? Description { get; set; }
        public int QuestionCount { get; set; }
        public decimal Price { get; set; }
    }
}
