﻿using MindSpace.Application.Specifications;

namespace MindSpace.Application.Features.ApplicationUsers.Specifications
{
    public class ApplicationUserSpecParams : BasePagingParams
    {
        private string _searchName;

        public string SearchName
        {
            get { return _searchName; }
            set { _searchName = value.Trim().ToLower(); }
        }

        public int? UserId { get; set; }

        public string? Sort { get; set; }
    }
}
