namespace RentACar.Web.Infastructure.Extensions
{
    using System.Reflection;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using RentACar.Data.Models;

    using static Common.ApplicationConstants;

    public static class WebApplicationBuilderExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, Type serviceType)
        {
            Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);
            if (serviceAssembly == null)
            {
                throw new InvalidOperationException("Invalid service type provided!");
            }

            Type[] serviceTypes = serviceAssembly
                .GetTypes()
                .Where(t => t.Name.EndsWith("Service") && !t.IsInterface)
                .ToArray();
            foreach (Type implementationType in serviceTypes)
            {
                Type? interfaceType = implementationType
                    .GetInterface($"I{implementationType.Name}");
                if (interfaceType == null)
                {
                    throw new InvalidOperationException($"No interface is provided for the service with name: {implementationType.Name}");
                }

                services.AddScoped(interfaceType, implementationType);
            }
        }
        /// <summary>
        /// This method seeds administrator role if it does not exist.
        /// Passed email should be of an existing user in the application.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static IApplicationBuilder SeedAdministrator(this IApplicationBuilder app, string email)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            IServiceProvider serviceProvider = scopedServices.ServiceProvider;

            UserManager<ApplicationUser> userManager =
                serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole<Guid>> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdminRoleName))
                {
                    return;
                }

                IdentityRole<Guid> role =
                new IdentityRole<Guid>(AdminRoleName);

                await roleManager.CreateAsync(role);

                ApplicationUser adminUser =
                    await userManager.FindByEmailAsync(email);

                await userManager.AddToRoleAsync(adminUser, AdminRoleName);
            })
                .GetAwaiter()
                .GetResult();

            return app;
        }

    }
}
