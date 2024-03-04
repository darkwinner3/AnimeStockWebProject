using AnimeStockWebProject.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using static AnimeStockWebProject.Common.GeneralAplicaitonConstants;

namespace AnimeStockWebProject.Extensions
{
    public static class WebApplicationBuilderExtension
    {
        public static IApplicationBuilder SeedAdministrator(this IApplicationBuilder app, string userId)
        {
            using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

            IServiceProvider serviceProvider = scopedServices.ServiceProvider;
            UserManager<User> userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole<Guid>> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdminRoleName))
                {
                    return;
                }
                IdentityRole<Guid> role = new IdentityRole<Guid>(AdminRoleName);
                await roleManager.CreateAsync(role);

                User userToFind = await userManager.FindByIdAsync(userId);
                await userManager.AddToRoleAsync(userToFind, AdminRoleName);
            })
                .GetAwaiter().GetResult();
            return app;
        }
    }
}
