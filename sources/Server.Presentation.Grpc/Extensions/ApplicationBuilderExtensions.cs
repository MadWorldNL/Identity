using MadWorldNL.Server.Domain.Authorizations;
using MadWorldNL.Server.Infrastructure.Database.Users;
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
    
    public static async Task AddFirstAdminAccount(this IApplicationBuilder app)
    {
        const string AdminUsername = "oveldman@gmail.com";
        
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();
        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUserExtended>>();

        var user = await userManager.FindByEmailAsync(AdminUsername);

        if (user is null)
        {
            user = new IdentityUserExtended()
            {
                UserName = AdminUsername,
                Email = AdminUsername
            };
            
            await userManager.CreateAsync(user);
        }

        var currentRoles = await userManager.GetRolesAsync(user);
        await userManager.AddRoleToUserAsync(user, Roles.IdentityAdminstrator, currentRoles);
    }
    
    private static async Task AddRoleToUserAsync(
        this UserManager<IdentityUserExtended> userManager,
        IdentityUserExtended user, 
        string role, 
        ICollection<string> currentRoles)
    {
        if (!currentRoles.Contains(role))
        {
            await userManager.AddToRoleAsync(user, role);
        }
    }

    private static async Task CreateRoleIfNotExists(this RoleManager<IdentityRole> roleManager, string role)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}