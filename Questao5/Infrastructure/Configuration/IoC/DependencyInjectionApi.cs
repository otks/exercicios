using Questao5.Infrastructure.Context;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Configuration.IoC;

public static class DependencyInjectionApi
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        // sqlite
        services.AddSingleton(new DatabaseConfig { Name = configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
        services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();
        
        services.AddSingleton<SqliteContext>();
        
        services.AddAccountMovementDependencyInjection();
        services.AddAccountDependencyInjection();
        services.AddIdemPotenceDependencyInjection();
        
        return services;
    }
}