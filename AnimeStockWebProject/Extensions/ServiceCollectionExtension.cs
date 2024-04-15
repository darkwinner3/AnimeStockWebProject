using AnimeStockWebProject.Areas.Admin.Contracts;
using AnimeStockWebProject.Areas.Admin.Services;
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
            serviceDescriptors.AddScoped<IUserAdminService, UserAdminService> ();
            serviceDescriptors.AddScoped<IBookAdminService, BookAdminService>();
            serviceDescriptors.AddScoped<IBookTagService, BookTagService>();
            serviceDescriptors.AddScoped<IBookTypeService, BookTypeService>();
            serviceDescriptors.AddScoped<IPictureAdminService, PictureAdminService>();

        }

        public static void ConfigureHangfire(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            RecurringJob.AddOrUpdate<IOrderService>("UpdateOrderStatus", orderService => orderService.UpdateOrderStatusAsync(), Cron.Hourly);
            RecurringJob.AddOrUpdate<IPictureAdminService>("DeletePictures", pictureAdminService => pictureAdminService.DeletePicturesAsync(), Cron.DayInterval(3));
            RecurringJob.AddOrUpdate<IBookAdminService>("DeleteBooks", bookAdminService => bookAdminService.DeleteBooksJobAsync(), Cron.Daily);
        }
    }
}
