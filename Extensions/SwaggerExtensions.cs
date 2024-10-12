using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace RunningTracker.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RunningTracker API", Version = "v1" });
                c.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "duration",
                    Example = new OpenApiString("01:00:00")
                });
            });
            return services;
        }
    }
}
