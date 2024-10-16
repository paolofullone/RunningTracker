using IntegratedTests.Configuration;
using IntegratedTests.Extensions;
using RunningTracker.Models;

namespace IntegratedTests.Controllers
{
    public class RunControllerTests : IClassFixture<TestApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        private const string BasePath = "/api/v1/run";
        private readonly TestApplicationFactory _applicationFactory;

        public RunControllerTests(TestApplicationFactory applicationFactory)
        {
            _httpClient = applicationFactory.CreateClient();
            _applicationFactory = applicationFactory;
        }

        [Fact]
        public async Task GetRuns_ShouldReturnRuns()
        {
            // Arrange
            var url = $"{BasePath}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            //Act
            var response = await _httpClient.SendAsync(request);

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetRunByDate_ShouldReturnRun()
        {
            // Arrange
            var datePath = "/date/2024-08-10";
            var url = $"{BasePath}{datePath}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            // Act
            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadContentAs<List<Run>>();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(new DateTime(2024, 08, 10), content.First().Date);
            Assert.Equal(10, content.First().Distance);
            Assert.Equal(6, content.First().Pace);

        }
    }
}
