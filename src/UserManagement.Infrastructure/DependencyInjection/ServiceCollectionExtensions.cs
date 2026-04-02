using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Interfaces;
using UserManagement.Infrastructure.Persistence;
using UserManagement.Infrastructure.Repositories;
using UserManagement.Infrastructure.Services;

namespace UserManagement.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString, sqlOptions =>
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null)));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
