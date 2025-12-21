using BlogSystem.Domain.Contracts;
using BlogSystem.Domain.Entities;
using BlogSystem.Domain.Entities.Content;
using BlogSystem.Presistence.Data.DbContexts;
using BlogSystem.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogSystem.Presistence.Data.SeedData
{
    public class DataInitializer : IDataInitializer
    {
        private readonly BlogDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<DataInitializer> _logger;

        public DataInitializer(BlogDbContext dbContext,
                                UserManager<ApplicationUser> userManager,
                                RoleManager<IdentityRole> roleManager,
                                ILogger<DataInitializer> logger)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task InitializeAsync(string filePath)
        {
            try
            {
                await SeedIdentityData();
                if( ! await _dbContext.Tags.AnyAsync())
                {
                    await SeedDataFromJsonAsync<Tag, int>("tags.json", filePath, _dbContext.Tags);
                }
                if( ! await _dbContext.Categories.AnyAsync())
                {
                    await SeedDataFromJsonAsync<Category, int>("categories.json", filePath, _dbContext.Categories);
                }
                await _dbContext.SaveChangesAsync();
                if ( ! await _dbContext.Posts.AnyAsync())
                {
                    await SeedDataFromJsonAsync<Post, int>("posts.json", filePath, _dbContext.Posts);
                }
                await _dbContext.SaveChangesAsync();
                if ( ! await _dbContext.PostTags.AnyAsync())
                {
                    await SeedDataFromJsonAsync<PostTag, int>("postTags.json", filePath, _dbContext.PostTags);
                }
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Data Seeding Failed {ex}");
            }
        }
        //Data/JsonFiles/SeedData
        private async Task SeedDataFromJsonAsync<TEntity,TKey>(string fileName,string filePath, DbSet<TEntity> dbSet) where TEntity : BaseEntity<TKey>
        {
            if (string.IsNullOrWhiteSpace(fileName)|| string.IsNullOrWhiteSpace(filePath))
                throw new FileNotFoundException($"Null, Empty OR WhiteSpace FileName argument {fileName}");
            if(Path.HasExtension(filePath))
                throw new ArgumentException($"filePath: '{filePath}' must be a directory path, not a file path.");

            var fullFilePath =Path.Combine(filePath, fileName); 
            if(!File.Exists(fullFilePath))
                throw new FileNotFoundException ( $"Seed file not found at path: {fullFilePath}");
            try
            {

                using var stream = File.OpenRead(fullFilePath);
                var jsonSelializerOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var data = await JsonSerializer.DeserializeAsync<List<TEntity>>(stream, jsonSelializerOptions);
                if (data is not null)
                {
                    dbSet.AddRange(data);
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Failed To Seed Data From Json => {ex.Message}");
            }
        }

        private async Task SeedIdentityData()
        {
            try
            {
             
                if (!await _roleManager.Roles.AnyAsync())
                {
                    await _roleManager.CreateAsync(new IdentityRole(UserRole.Admin.ToString()));
                    await _roleManager.CreateAsync(new IdentityRole(UserRole.Reader.ToString()));
                    await _roleManager.CreateAsync(new IdentityRole(UserRole.Editor.ToString()));
                }
                if (!await _userManager.Users.AnyAsync())
                {
                    var user_admin = new ApplicationUser
                    {
                        UserName = "admin_rojena",
                        Email = "admin@blog.com"
                    };
                    var user_editor = new ApplicationUser
                    {
                        UserName = "editor_john",
                        Email = "john.editor@blog.com"
                    };
                    var user_reader = new ApplicationUser
                    {
                        UserName = "reader_dina",
                        Email = "dina.reader@blog.com"
                    };
                    await _userManager.CreateAsync(user_admin, "P@ssw0rd");
                    await _userManager.AddToRoleAsync(user_admin,UserRole.Admin.ToString());

                    await _userManager.CreateAsync(user_editor, "P@ssw0rd");
                    await _userManager.AddToRoleAsync(user_editor, UserRole.Editor.ToString());

                    await _userManager.CreateAsync(user_reader, "P@ssw0rd");
                    await _userManager.AddToRoleAsync(user_reader, UserRole.Reader.ToString());

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error While Seeding Identity Dataase : Message = {ex.Message}");
            }

        }

        


    }
}
