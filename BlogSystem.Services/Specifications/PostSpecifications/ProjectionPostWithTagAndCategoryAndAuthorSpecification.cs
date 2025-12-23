using BlogSystem.Domain.Entities.Content;
using BlogSystem.Shared.DTOs;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Services.Specifications.PostSpecifications
{
    internal class ProjectionPostWithTagAndCategoryAndAuthorSpecification:ProjectionBaseSpecification<Post,int,PostDTO>
    {
        public ProjectionPostWithTagAndCategoryAndAuthorSpecification(PostQueryParameters queryParameters) : base(CreateCriteria(queryParameters))
        {
            AddSelector(createSelector());
        }
        //create select
        private Expression<Func<Post, PostDTO>> createSelector()
        {
            return post => new PostDTO
            {
                Id=post.Id,
                Content=post.Content,
                Title=post.Title,
                CreatedAt=post.CreatedAt,
                UpdatedAt=post.UpdatedAt,
                Status=post.Status.ToString(),
                Category = post.Category.Name,
                AuthorId=post.AuthorId,
                AuthorName = post.Author.UserName,
                PostTags=post.PostTags.Select(t => new TagDTO
                {
                    Id=t.TagId,
                    Name=t.Tag.Name
                }).ToList()
             


            };
        }
        //
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
