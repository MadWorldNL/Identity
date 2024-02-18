namespace MadWorldNL.Server.Presentation.Grpc;

public static class WebApplicationBuilderExtensions
{
    public static string BuildConnectionString(this WebApplicationBuilder builder, string connectionStringName)
    {
        var connectionString = builder.Configuration.GetValue<string>($"DbContext:{connectionStringName}")!;
        var password = builder.Configuration.GetValue<string>("DbContext:Password")!;
        return connectionString.Replace("{password}", password);
    }
}