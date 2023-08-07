using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Practical.Notification.Api.Filters;

public class SecurityRequirementsOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Check if the operation has the [Authorize] attribute
        var hasAuthorizeAttribute = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
            .Union(context.MethodInfo.GetCustomAttributes(true))
            .OfType<AuthorizeAttribute>()
            .Any();

        if (hasAuthorizeAttribute)
        {
            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    // Use "Bearer" as the security scheme name
                    [ new OpenApiSecurityScheme { Scheme = "Bearer" }] = new List<string>()
                }
            };
        }
    }
}
