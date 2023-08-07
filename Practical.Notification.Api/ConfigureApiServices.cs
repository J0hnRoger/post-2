using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace Practical.Notification.Api;

public static class ConfigureApiServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddRazorPages();
        
        services.AddSwaggerGen();
        
        services.AddEndpointsApiExplorer();

        return services;
    }
}