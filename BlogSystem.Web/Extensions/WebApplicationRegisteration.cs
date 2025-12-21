using BlogSystem.Domain.Contracts;

namespace BlogSystem.Web.Extensions
{
    public static class WebApplicationRegisteration
    {
        public static async Task<WebApplication> SeedDataAsync(this WebApplication app,IConfiguration configuration,IWebHostEnvironment webHostEnvironment)
        {
          await using var scope = app.Services.CreateAsyncScope();
           var service= scope.ServiceProvider.GetRequiredService<IDataInitializer>();
            var path = configuration["SeedData:Path"];
            var pathWithContentRootPath=Path.Combine(webHostEnvironment.ContentRootPath,path);
           await service.InitializeAsync(pathWithContentRootPath);
            return app;
        }
    }
}
