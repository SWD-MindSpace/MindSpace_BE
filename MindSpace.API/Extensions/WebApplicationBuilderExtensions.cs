using Microsoft.OpenApi.Models;

namespace MindSpace.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen(c =>
                    {
                        //add bearer authentication to swagger 
                        c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                        {
                            Type = SecuritySchemeType.Http,
                            Scheme = "Bearer"
                        });

                        //tells swagger that all requests will received a bearer token if theres one - this does not mean the request must have a token
                        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "bearerAuth"
                            }
                        },
                        []
                    }
                            });
                    });
            //tell swagger to support minimal apis, which the Identity apis are.
            builder.Services.AddEndpointsApiExplorer();

            //builder.Host.UseSerilog((context, configuration) =>
            //{
            //    configuration.ReadFrom.Configuration(context.Configuration);
            //});
        }
    }
}
