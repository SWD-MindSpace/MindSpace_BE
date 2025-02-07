﻿using MindSpace.Application.Specifications;
using MindSpace.Domain.Entities.Identity;
using System.Linq.Expressions;


namespace MindSpace.Application.Features.ApplicationUsers.Specifications
{
    public class ApplicationUserSpecification : BaseSpecification<ApplicationUser>
    {
        /// <summary>
        /// Search By User Id
        /// </summary>
        /// <param name="id"></param>
        public ApplicationUserSpecification(int id) : base(x => x.Id.Equals(id))
        {
            // will add AddInclude later
            // AddInclude(x => x.School)
        }

        /// <summary>
        /// Search By email
        /// </summary>
        /// <param name="id"></param>
        public ApplicationUserSpecification(string email) : base(x => x.Email.Equals(email))
        {
            // will add AddInclude later
            // AddInclude(x => x.School)
        }


        /// <summary>
        /// Search By General Filter
        /// </summary>
        /// <param name="specParams"></param>
        public ApplicationUserSpecification(ApplicationUserSpecParams specParams) : base(s =>

            string.IsNullOrEmpty(specParams.SearchName) ||
            s.Email.ToLower().Contains(specParams.SearchName.ToLower()) ||
            s.UserName.ToLower().Contains(specParams.SearchName.ToLower()) ||
            s.FullName.ToLower().Contains(specParams.SearchName.ToLower()) ||
            s.Id.Equals(specParams.UserId))
        {

            // will add AddInclude later
            // AddInclude(x => x.School)

            // Add Sorting
            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "dobAsc":
                        AddOrderBy(x => x.DateOfBirth.ToString()); break;
                    case "dobDesc":
                        AddOrderByDescending(x => x.DateOfBirth.ToString()); break;
                    default:
                        AddOrderBy(x => x.FullName); break; // default is sort by full name
                }
            }
        }


    }
}
