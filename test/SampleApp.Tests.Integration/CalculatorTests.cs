using Microsoft.AspNetCore.Mvc.Testing;

namespace SampleApp.Tests.Integration;

public class CalculatorTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public CalculatorTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetSum1()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/Calculator/Sum?number1=1&number2=1");
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("2", content);
    }

    [Fact]
    public async Task GetSum2()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/Calculator/Sum?number1=2&number2=3");
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("5", content);
    }
}