namespace IdentityGenerator.Data;

public record Address
{
    public int Id { get; init; }
    public string Country { get; init; } = "";
    public string Region { get; init; } = "";
    public string District { get; init; } = "";
    public string City { get; init; } = "";
    public string Street { get; init; } = "";
    public string Number { get; init; } = "";
    public string Floats { get; init; } = "";
    public string Postcode { get; init; } = "";
}
