using Microsoft.Extensions.DependencyInjection;

namespace MoleRouter;

public static class MoleRouterBootstrap
{
    public static void AddMoleRouter(this IServiceCollection services, int id)
    {
        services.AddSingleton<IMoleClient, MoleClient>();
    }
}