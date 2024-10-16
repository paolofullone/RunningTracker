using IntegratedTests.Configuration;

namespace IntegratedTests.ClassFixtures;

[CollectionDefinition(nameof(IntegrationTestCollection))]
public class IntegrationTestCollection : ICollectionFixture<TestApplicationFactory>
{
}
