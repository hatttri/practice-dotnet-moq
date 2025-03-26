using SampleApp.Controllers;

namespace SampleApp.Tests.Unit.Controllers;

public class CalculatorControllerTests
{
    [Fact]
    public void GetSum()
    {
        var controller = new CalculatorController();
        Assert.Equal(2, controller.GetSum(1, 1));
        Assert.Equal(5, controller.GetSum(2, 3));
    }
}