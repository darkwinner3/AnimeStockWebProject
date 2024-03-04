using AnimeStockWebProject.Core.Contracts;
using AnimeStockWebProject.Core.Services;

namespace AnimeStockWebProject.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddServices(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<IUserService, UserService>();
        }
    }
}
