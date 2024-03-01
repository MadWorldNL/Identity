using Grpc.Net.Client;
using MadWorldNL.Server.Infrastructure.Database;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Respawn;
using Testcontainers.PostgreSql;

namespace MadWorldNL.Server.Presentation.Grpc.IntegrationTests.TestBase;

public class GrpcFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private HttpMessageHandler? _handler;
    private GrpcChannel? _channel;
    private Respawner? _respawner = default!;
    private NpgsqlConnection _connection = default!;
    
    public GrpcChannel Channel => _channel ??= CreateChannel();
    
    private GrpcChannel CreateChannel()
    {
        _handler = Server.CreateHandler();
        
        return GrpcChannel.ForAddress(Server.BaseAddress!, new GrpcChannelOptions
        {
            HttpHandler = _handler
        });
    }
    
    protected readonly PostgreSqlContainer PostgreSqlContainer = PostgreSqlContainerBuilder.Build();
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(UserDbContext));
            services.RemoveAll(typeof(DbContextOptions<UserDbContext>));

            services.AddDbContext<UserDbContext>(options =>
                options.UseNpgsql(PostgreSqlContainer.GetConnectionString()));

            services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
        });

        base.ConfigureWebHost(builder);
    }
    
    public async Task InitializeAsync()
    {
        await PostgreSqlContainer.StartAsync();
        _connection = new NpgsqlConnection(PostgreSqlContainer.GetConnectionString());
        await _connection.OpenAsync();

        Server.CreateClient();
        
        _respawner = await Respawner.CreateAsync(_connection, new RespawnerOptions()
        {
            DbAdapter = DbAdapter.Postgres
        });
    }
    
    public IServiceScope CreateScope()
    {
        var scopeFactory = Server.Services.GetService<IServiceScopeFactory>()!;
        return scopeFactory.CreateScope();
    }
    
    public async Task ResetDatabase()
    {
        await _respawner!.ResetAsync(_connection);
    }
    
    public async Task DisposeAsync()
    {
        await PostgreSqlContainer.DisposeAsync();
    }
}