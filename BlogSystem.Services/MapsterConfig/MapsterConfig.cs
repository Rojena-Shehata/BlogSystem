using BlogSystem.Domain.Entities;
using BlogSystem.Domain.Entities.Content;
using BlogSystem.Shared.DTOs;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace BlogSystem.Services.MapsterConfig
{
    public static class MapsterConfig
    {
        public static void Configure()
        {
            ConfigurePost();
            ConfigureTag();

        }
        private static void ConfigureTag()
        {
            TypeAdapterConfig<Tag, TagDTO>.NewConfig();
        }

        private static void ConfigurePost()
        {

            TypeAdapterConfig<Post, PostDTO>.NewConfig()
                .Map(dest => dest.PostTags, src => src.Tags)
                .Map(dest=>dest.AuthorId,src=>src.Author.Id)
                .Map(dest=>dest.AuthorName,src=>src.Author.UserName)
                .Map(dest=>dest.Category,src=>src.Category.Name)
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
