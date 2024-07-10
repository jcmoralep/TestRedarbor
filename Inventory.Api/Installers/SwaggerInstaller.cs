using System.Reflection;
using Common.Application.Enum;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Inventory.Api.Installers;

public class SwaggerInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Iventory API",
                Version = "v1",
                Description = "Services Inventory"
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Auth Header using Bearer scheme",
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Name = "Authorization",
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name= "Bearer",
                            Type= SecuritySchemeType.ApiKey,
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });

            c.MapType<MovementType>(() => new OpenApiSchema
            {
                Type = "integer",
                Enum = new List<IOpenApiAny>
                {
                    new OpenApiInteger((int)MovementType.In),
                    new OpenApiInteger((int)MovementType.Out),
                },
                Description = "MovementType Enum: 1 = Ingreso, 2 = Salida"
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }
}
