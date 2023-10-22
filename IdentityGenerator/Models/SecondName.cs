namespace IdentityGenerator.Models;

public record SecondName
{
    public int Id { get; init; }
    public string Country { get; init; } = "";
    public string Gender { get; init; } = "";
    public string Text { get; init; } = "";
}
