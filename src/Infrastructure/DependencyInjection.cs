using BookStoreApi.Application.Common.Interfaces.Contexts;
using BookStoreApi.Application.Common.Interfaces.Repositories;
using BookStoreApi.Infrastructure.Contexts;
using BookStoreApi.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStoreApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBookStoreContext, BookStoreContext>();
        services.AddScoped<IBookRepository, BookRepository>();
        return services;
    }
}