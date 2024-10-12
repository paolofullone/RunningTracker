using RunningTracker.Utils;
using System.Text.Json.Serialization;

namespace RunningTracker.Extensions
{
    public static class MvcExtensions
    {
        public static IServiceCollection AddCustomControllers(this IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.Converters.Add(new JsonTimeSpanConverter());
                });
            return services;
        }
    }
}
