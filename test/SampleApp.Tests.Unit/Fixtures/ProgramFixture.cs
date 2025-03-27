using Microsoft.Extensions.DependencyInjection;

namespace SampleApp.Tests.Unit.Fixtures;

public class ProgramFixture : IDisposable
{
    private bool _disposed;

    public ServiceProvider Services { get; }

    public ProgramFixture()
    {
        var services = new ServiceCollection();
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