using Microsoft.AspNetCore.Mvc;

using SampleApp.DbContexts;

namespace SampleApp.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly SampleDb _context;

    public CalculatorController(SampleDb context)
    {
        _context = context;
    }

    [HttpGet("Sum")]
    public int GetSum(int number1, int number2)
    {
        var user = _context.Users.FirstOrDefault();

        return number1 + number2;
    }
}