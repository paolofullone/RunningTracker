using Microsoft.Extensions.Configuration;

namespace FunctionalTests.Config
{
    public class ServicesConfig
    {
        private const string FilePath = "appsettings-functional.json";

        public ServicesConfig() 
        {
            var config = new ConfigurationBuilder();
            config.AddJsonFile(FilePath);
            config.AddEnvironmentVariables(); // not used at this moment, to a future deploy

            config.Build().Bind(this);
        }

        public string ApiHost { get; set; }
    }
}
