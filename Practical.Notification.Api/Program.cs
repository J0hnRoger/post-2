using Microsoft.OpenApi.Models;
using Practical.Notification.Infrastructure;
using Practical.Notification.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notification API", Version = "v1" });

        // Configure Bearer Token authentication
         c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
         {
             Name = "Authorization",
             Type = SecuritySchemeType.ApiKey,
             Scheme = "Bearer",
             BearerFormat = "JWT",
             In = ParameterLocation.Header,
             Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
         });
         c.AddSecurityRequirement(new OpenApiSecurityRequirement
         {
             {
                   new OpenApiSecurityScheme
                     {
                         Reference = new OpenApiReference
                         {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                         }
                     },
                     new string[] {}
             }
         }); 
    });

var app = builder.Build();

// Initialise and seed database
using (var scope = app.Services.CreateScope())
{
    var initialiser = scope.ServiceProvider.GetRequiredService<PortalContextInitialiser>();
    await initialiser.InitialiseAsync();
}    

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();