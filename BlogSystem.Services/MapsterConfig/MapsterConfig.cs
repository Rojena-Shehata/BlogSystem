using BlogSystem.Domain.Entities;
using BlogSystem.Domain.Entities.Content;
using BlogSystem.Shared.DTOs;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlogSystem.Services.MapsterConfig
{
    public static class MapsterConfig
    {
        public static void Configure()
        {
            ConfigurePost();

        }
        private static void ConfigurePost()
        {

            TypeAdapterConfig<Post, PostDTO>.NewConfig()
                .Map(dest => dest.PostTags, src => GetTagsInString(src.PostTags))
                .Map(dest=>dest.AuthorId,src=>src.Author.UserName)
                .Map(dest=>dest.CreatedAt,src=>src.CreatedAt.ToString("G"))
                .Map(dest=>dest.Status,src=>src.Status.ToString());



        }
        private static string GetTagsInString(ICollection<PostTag>? Tags)
        {
            if (Tags is null|| Tags.Count<=0)           
                return string.Empty;
            var newTag = new StringBuilder();
            foreach (var tag in Tags)
            {
                newTag.Append($"#{tag.Tag.Name}");
            }
            return newTag.ToString();
        }
    }
}
