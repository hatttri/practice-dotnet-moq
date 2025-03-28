using Microsoft.Extensions.DependencyInjection;

using SampleApp.DbContexts;
using SampleApp.DbModels;
using SampleApp.DbServices;
using SampleApp.Tests.Unit.Fixtures;

namespace SampleApp.Tests.Unit.DbServices;

[Collection("Program collection")]
public class UserServiceTests(
    ProgramFixture fixture
)
{
    // テストデータ
    private readonly User User1 = new User { Id = Guid.NewGuid(), Name = "Test1" };
    private readonly User User2 = new User { Id = Guid.NewGuid(), Name = "Test2" };

    private SampleDb GetSampleDb()
    {
        return fixture.Services.GetRequiredService<SampleDb>();
    }

    [Fact]
    public async Task InsertUserAsync_Returns_User()
    {
        // テストデータ生成
        var sampleDb = GetSampleDb();

        // データ作成
        var usersService = new UsersService(sampleDb);
        var user = await usersService.InsertUserAsync(User1.Name);

        // 検証
        Assert.NotNull(user);
        Assert.IsType<Guid>(user.Id);
        Assert.NotEqual(Guid.Empty, user.Id);
        Assert.Equal(User1.Name, user.Name);

        // データ取得
        var userFromDb = await usersService.SelectUserAsync(user.Id);

        // 検証
        Assert.NotNull(userFromDb);
        Assert.Equal(user.Id, userFromDb.Id);
        Assert.Equal(User1.Name, userFromDb.Name);
    }

    [Fact]
    public async Task SelectUserAsync_Returns_Null()
    {
        // テストデータ生成
        var sampleDb = GetSampleDb();
        sampleDb.AddRange(User1, User2);
        await sampleDb.SaveChangesAsync();

        // データ取得
        var usersService = new UsersService(sampleDb);
        User? user = await usersService.SelectUserAsync(Guid.NewGuid());

        // 検証
        Assert.Null(user);
    }

    [Fact]
    public async Task SelectUserAsync_Returns_User()
    {
        // テストデータ生成
        var sampleDb = GetSampleDb();
        sampleDb.AddRange(User1, User2);
        await sampleDb.SaveChangesAsync();

        // データ取得
        var usersService = new UsersService(sampleDb);
        var user = await usersService.SelectUserAsync(User1.Id);

        // 検証
        Assert.NotNull(user);
        Assert.Equal(User1.Id, user.Id);
        Assert.Equal(User1.Name, user.Name);
    }

    [Fact]
    public async Task SelectUsersAsync_Returns_Empty()
    {
        // テストデータ生成
        var sampleDb = GetSampleDb();

        // データ取得
        var usersService = new UsersService(sampleDb);
        var users = await usersService.SelectUsersAsync();

        // 検証
        Assert.Empty(users);
    }

    [Fact]
    public async Task SelectUsersAsync_Returns_Users()
    {
        // テストデータ生成
        var sampleDb = GetSampleDb();
        sampleDb.AddRange(User1, User2);
        await sampleDb.SaveChangesAsync();

        // データ取得
        var usersService = new UsersService(sampleDb);
        var users = await usersService.SelectUsersAsync();

        // 検証
        Assert.Equal(2, users.Count());

        var user1 = users.FirstOrDefault(u => u.Id == User1.Id);
        Assert.NotNull(user1);
        Assert.Equal(User1.Id, user1.Id);
        Assert.Equal(User1.Name, user1.Name);

        var user2 = users.FirstOrDefault(u => u.Id == User2.Id);
        Assert.NotNull(user2);
        Assert.Equal(User2.Id, user2.Id);
        Assert.Equal(User2.Name, user2.Name);
    }
}