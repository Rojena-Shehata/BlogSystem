using BlogSystem.Domain.Entities.Content;
using BlogSystem.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Services.Specifications.PostSpecifications
{
    internal class PostWithTagAndCategoryAndAuthor : BaseSpecification<Post, int>
    {
        internal PostWithTagAndCategoryAndAuthor(PostQueryParameters queryParameters) : base(CreateCriteria(queryParameters))
        {
            AddInclude(post => post.Category);
            AddInclude(post => post.Author);
            AddInclude(post => post.Tags);
        }

        private static Expression<Func<Post, bool>> CreateCriteria(PostQueryParameters queryParameters)
        {
            return post => (!queryParameters.CategoryId.HasValue || queryParameters.CategoryId == post.CategoryId) &&

                    (!queryParameters.TagId.HasValue || post.PostTags.Any(x => x.TagId == queryParameters.TagId)) &&

                    (string.IsNullOrWhiteSpace(queryParameters.Search) ||
                       post.Title.ToLower().Contains(queryParameters.Search.ToLower()) ||
                       post.Content.Contains(queryParameters.Search)) &&

                    (!queryParameters.Status.HasValue || queryParameters.Status == post.Status);
                    
        }

    }
}
