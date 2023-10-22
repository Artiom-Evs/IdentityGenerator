using IdentityGenerator.Data;
using IdentityGenerator.Extensions;
using IdentityGenerator.Models;

namespace IdentityGenerator.Services;

public class BYGenerationProvider : IGenerationProvider
{
    private readonly RegionalDataDbContext _context;

    public BYGenerationProvider(RegionalDataDbContext context)
    {
        _context = context;
    }

    public string Region => "BY";

    public IEnumerable<FakeDataItem> GenerateFakeItems(int startItem, int itemsCount, int seedNumber)
    {
        return Enumerable.Range(0, itemsCount)
            .Select(i => GenerateSingleFakeItem(i + startItem + seedNumber));
    }

    private FakeDataItem GenerateSingleFakeItem(int seedNumber)
    {
        Random rnd = new(seedNumber);
        string gender = rnd.Next() % 2 == 0 ? "M" : "F";

        FirstName firstName = _context.GetRandomFirstName(rnd, this.Region, gender);
        SecondName secondName = _context.GetRandomSecondName(rnd, this.Region, gender);
        Address address = _context.GetRandomAddress(rnd, this.Region);

        string formattedName = FormatName(firstName, secondName, rnd);
        string formattedAddress = FormatAddress(address, rnd);
        string phone = GeneratePhoneNumber(rnd);

        return new FakeDataItem()
        {
            Name = formattedName,
            Address = formattedAddress,
            Phone = phone,
        };
    }

    private string FormatName(FirstName firstName, SecondName secondName, Random rnd)
    {
        return rnd.Next(0, 2) == 0 ? $"{secondName.Text} {firstName.Text}" : $"{firstName.Text} {secondName.Text}";
    }
    
    private string FormatAddress(Address address, Random rnd)
    {
        string postcode = string.IsNullOrEmpty(address.Postcode) && rnd.Next(0, 2) == 0 ? "" : $", {address.Postcode}";
        string country = rnd.Next(0, 4) switch
        {
            0 => "BY",
            1 => "РБ",
            2 => "Беларусь",
            _ => "Рэспубліка Беларусь"
        };
        string region = string.IsNullOrEmpty(address.Region) && rnd.Next(0, 2) == 0 ? "" : $", {address.Region}";
        string District = string.IsNullOrEmpty(address.District) && rnd.Next(0, 2) == 0 ? "" : $", {address.District}";

        return rnd.Next(0, 2) switch
        {
            0 => $"{address.Street} {address.Number}, {address.City}{District}{region}, {country}{postcode}",
            _ => $"{address.City}, {address.Street} {address.Number}, {District}{region}{postcode}"
        };
    }

    private string GeneratePhoneNumber(Random rnd)
    {
        string format = rnd.Next(0, 3) switch
        {
            0 => "+375",
            1 => "375",
            _ => "80"
        };
        string provider = rnd.Next(0, 5) switch
        {
            0 => "17",
            1 => "25",
            2 => "29",
            3 => "33",
            _ => "44"
        };
        string main = rnd.Next(1_000_000, 10_000_000).ToString();

        return rnd.Next(0, 7) switch
        {
            0 => $"{format} ({provider}) {main[..3]} {main[3..5]} {main[5..7]}",
            1 => $"{format} {provider} {main[..3]} {main[3..5]} {main[5..7]}",
            2 => $"{format} ({provider}) {main[..3]} {main[3..5]}{main[5..7]}",
            3 => $"{format} {provider} {main[..3]} {main[3..5]}{main[5..7]}",
            4 => $"{format} ({provider}) {main[..3]}{main[3..5]}{main[5..7]}",
            5 => $"{format} {provider} {main[..3]}{main[3..5]}{main[5..7]}",
            _ => $"{format}{provider}{main[..3]}{main[3..5]}{main[5..7]}"
        };
    }
}
