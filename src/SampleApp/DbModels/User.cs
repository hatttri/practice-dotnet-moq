namespace SampleApp.DbModels;

public class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}