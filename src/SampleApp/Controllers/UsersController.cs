using Microsoft.AspNetCore.Mvc;

using SampleApp.DbModels;
using SampleApp.DbServices;

namespace SampleApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(
    UsersService usersService
) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserAsync([FromRoute] Guid id)
    {
        User? user = await usersService.SelectUserAsync(id);
        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync()
    {
        IEnumerable<User> users = await usersService.SelectUsersAsync();
        return Ok(users);
    }

    [HttpPost]
    public async Task<ActionResult<User>> PostUserAsync([FromBody] string name)
    {
        User user = await usersService.InsertUserAsync(name);
        return Ok(user);
    }
}