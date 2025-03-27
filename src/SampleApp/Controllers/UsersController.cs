using Microsoft.AspNetCore.Mvc;

using SampleApp.DbContexts;
using SampleApp.DbModels;

namespace SampleApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly SampleDb _sampleDb;

    public UsersController(SampleDb sampleDb)
    {
        _sampleDb = sampleDb;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        var users = _sampleDb.Users;
        return Ok(users);
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUser([FromRoute] Guid id)
    {
        var user = _sampleDb.Users.Where(u => u.Id == id).FirstOrDefault();
        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public ActionResult<User> PostUser([FromBody] string name)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = name
        };

        _sampleDb.Users.Add(user);
        _sampleDb.SaveChanges();

        return Ok(user);
    }
}