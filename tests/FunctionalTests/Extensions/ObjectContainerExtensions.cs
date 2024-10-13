using FunctionalTests.Apis;
using FunctionalTests.Config;
using Refit;
using Reqnroll.BoDi;
using System.Text.Json;

namespace FunctionalTests.Extensions
{
    public static class ObjectContainerExtensions
    {
        public static IObjectContainer AddConfig(this IObjectContainer container)
        {
            container.RegisterInstanceAs(new ServicesConfig());
            return container;
        }

        public static IObjectContainer AddRunningTrackerApi(this IObjectContainer container)
        {
            var config = container.Resolve<ServicesConfig>();

            var managementHttpClient = new HttpClient { BaseAddress = new Uri(config.ApiHost) };

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var api = RestService.For<IRunningTrackerApi>(managementHttpClient, new RefitSettings
            {
                ContentSerializer = new SystemTextJsonContentSerializer(options),
            });

            container.RegisterInstanceAs(api);

            return container;
        }
    }
}
