using Microsoft.EntityFrameworkCore;

using SampleApp.DbModels;

namespace SampleApp.DbContexts;

public class SampleDb : DbContext
{
    public DbSet<User> Users { get; set; }

    public SampleDb(DbContextOptions<SampleDb> options) : base(options) { }
}