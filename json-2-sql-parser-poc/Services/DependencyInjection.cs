using json_2_sql_parser_poc.Services.Interfaces;

namespace json_2_sql_parser_poc.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddHttpSerivces(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IServiceConnectPoC, ServiceConnectPoC>();
        services.AddScoped<ITridionDocsPoC, TridionDocsPoC>();

        return services;
    }
}
