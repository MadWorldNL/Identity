using Microsoft.AspNetCore.Components.Authorization;

namespace MadWorldNL.Clients.Admin.Application.Authentications;

public class UserServiceMiddleware
{
    private readonly RequestDelegate next;

    public UserServiceMiddleware(RequestDelegate next)
    {
        this.next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext context, UserService service, AuthenticationStateProvider provider)
    {
        service.SetUser(context.User);
        await next(context);
    }
}