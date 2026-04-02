namespace DesignPatterns.Architectural.Repository;

// Simple entity used by the repository.
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public override string ToString() =>
        $"Student [Id={Id}, Name={Name}, Email={Email}]";
}