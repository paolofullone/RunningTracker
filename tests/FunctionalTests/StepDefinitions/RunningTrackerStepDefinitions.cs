using FunctionalTests.Drivers;
using Reqnroll;
using RunningTracker.Dto;

namespace FunctionalTests.StepDefinitions
{
    [Binding]
    public class RunningTrackerStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly RunningTrackerDriver _driver;
        private readonly CancellationToken _cancellationToken;

        public RunningTrackerStepDefinitions(ScenarioContext scenarioContext, RunningTrackerDriver driver)
        {
            _scenarioContext = scenarioContext;
            _driver = driver;
            _cancellationToken = CancellationToken.None;
        }

        [Given("That we want to insert some runs:")]
        public async Task GivenThatWeWantToInsertSomenRuns(DataTable dataTable)
        {
            var runRequests = dataTable.CreateSet<RunDto>().ToList();
            _scenarioContext.Set(runRequests, "runRequests");
        }

        [When("We request to insert some runs")]
        public async Task WhenWeRequestThePOSTApiEndpoint()
        {
            var runRequests = _scenarioContext.Get<IEnumerable<RunDto>>("runRequests");
            foreach (var runDto in runRequests)
            {
                await _driver.AddRun(runDto);
            }
        }

        [Then("The runs are inserted, then the test validates and deletes them")]
        public async Task ThenARunIsStoredInDatabase()
        {
            await _driver.ValidateSucecess(_cancellationToken);
        }
    }
}
