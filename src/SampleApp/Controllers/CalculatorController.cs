using Microsoft.AspNetCore.Mvc;

namespace SampleApp.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculatorController : ControllerBase
{
    public CalculatorController() { }

    [HttpGet("Sum")]
    public int GetSum(int number1, int number2)
    {
        return number1 + number2;
    }
}