using MadWorldNL.Server.Domain.Authorizations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MadWorldNL.Server.Presentation.Grpc.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void MigrateDatabase<TDbContext>(this IApplicationBuilder app) where TDbContext : DbContext
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<TDbContext>();
        context.Database.Migrate();
    }

    public static async Task AddAllIdentityRoles(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();
        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await roleManager.CreateRoleIfNotExists(Roles.IdentityAdminstrator);
    }

    private static async Task CreateRoleIfNotExists(this RoleManager<IdentityRole> roleManager, string role)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}