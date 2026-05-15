namespace TestProject1;

public class BaseIntegrationTest : IClassFixture<LetstudyWebApplicationFactory>
{
    protected readonly LetstudyWebApplicationFactory _factory;
    protected readonly HttpClient _client;

    protected BaseIntegrationTest(LetstudyWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }
}