using Microsoft.EntityFrameworkCore;

using SampleApp.DbContexts;
using SampleApp.DbModels;

namespace SampleApp.DbServices;

public class UsersService(
    SampleDb sampleDb
)
{
    public async Task<User> InsertUserAsync(string name)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = name
        };

        sampleDb.Users.Add(user);
        await sampleDb.SaveChangesAsync();

        return user;
    }

    public async Task<User?> SelectUserAsync(Guid id)
    {
        User? user = await sampleDb.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        return user;
    }

    public async Task<IEnumerable<User>> SelectUsersAsync()
    {
        IEnumerable<User> users = await sampleDb.Users.ToListAsync();
        return users;
    }
}