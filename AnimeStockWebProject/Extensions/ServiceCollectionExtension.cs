using AnimeStockWebProject.Core.Contracts;
using AnimeStockWebProject.Core.Services;
using Hangfire;

namespace AnimeStockWebProject.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddServices(this IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            // Use IConfiguration
            serviceDescriptors.AddHangfire(config => config.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"))); 

            serviceDescriptors.AddScoped<IUserService, UserService>();
            serviceDescriptors.AddScoped<ITagService, TagService>();
            serviceDescriptors.AddScoped<IBookService, BookService>();
            serviceDescriptors.AddScoped<ITypeService, TypeService>();
            serviceDescriptors.AddScoped<ICommentService, CommentService>();
            serviceDescriptors.AddScoped<IOrderService, OrderService>();

        }

        public static void ConfigureHangfire(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard();

            RecurringJob.AddOrUpdate<IOrderService>("UpdateOrderStatus", orderService => orderService.UpdateOrderStatusAsunc(), Cron.Hourly);
        }
    }
}
