using Xunit;

namespace TestProject.API.Tests.Infrastructures
{
    [CollectionDefinition(nameof(TestProjectApiTestCollection))]
    public class TestProjectApiTestCollection
        : ICollectionFixture<TestProjectApiFixture>
    {
    }
}
