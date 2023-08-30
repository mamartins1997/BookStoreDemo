using Microsoft.OpenApi.Models;

namespace BookStoreApi.Host.Configuration;

public static class SwaggerConfig
{
    public static IServiceCollection ConfigureSwaggerServices(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bookstore API", Version = "v1" });
        });

        return services;
    }
    
    public static  IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bookstore API v1");
        });

        return app;
    }
}