using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using SampleApp.DbContexts;
using SampleApp.DbServices;

namespace SampleApp.Tests.Unit.Fixtures;

public class ProgramFixture : IDisposable
{
    private bool _disposed;

    public ServiceProvider Services { get; }

    public ProgramFixture()
    {
        var services = new ServiceCollection();

        services.AddTransient<SampleDb>(_ =>
        {
            var contextOptions = new DbContextOptionsBuilder<SampleDb>().UseSqlite("Data Source=:memory:").Options;
            var context = new SampleDb(contextOptions);

            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            return context;
        });
        services.AddTransient<UsersService>();

        Services = services.BuildServiceProvider();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing) { }

        _disposed = true;
    }
}

[CollectionDefinition("Program collection")]
public class DatabaseCollection : ICollectionFixture<ProgramFixture> { }