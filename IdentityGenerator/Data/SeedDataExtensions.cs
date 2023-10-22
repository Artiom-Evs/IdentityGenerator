using IdentityGenerator.Models;

namespace IdentityGenerator.Data;

public static class SeedDataExtensions
{
    public static WebApplication SeedRegionalData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<RegionalDataDbContext>();

        if (context.Addresses.Any())
        {
            return app;
        }

        var firstNames = GetFirstNamesFromCsv();
        var secondNames = GetSecondNamesFromCsv();
        var addresses = GetAddressesFromCsv();

        context.FirstNames.AddRange(firstNames);
        context.SecondNames.AddRange(secondNames);
        context.Addresses.AddRange(addresses);
        
        context.SaveChanges();
        return app;
    }

    private static IEnumerable<FirstName> GetFirstNamesFromCsv()
    {
        string path = $@"./data_source/first_names.csv";
        string[] lines = File.ReadAllLines(path);
        char separator = lines[0][0];
        
        var first_names = lines
            .Skip(1)
            .Select(l => l.Split(separator))
            .Select(arr => new FirstName()
            {
                Country = arr[0],
                Text = arr[1],
                Gender = arr[2]
            });

        return first_names;
    }

    private static IEnumerable<SecondName> GetSecondNamesFromCsv()
    {
        string path = $@"./data_source/second_names.csv";
        string[] lines = File.ReadAllLines(path);
        char separator = lines[0][0];

        var first_names = lines
            .Skip(1)
            .Select(l => l.Split(separator))
            .Select(arr => new SecondName()
            {
                Country = arr[0],
                Text = arr[1],
                Gender = arr[2]
            });

        return first_names;
    }


    private static IEnumerable<Address> GetAddressesFromCsv()
    {
        string path = $@"./data_source/addresses.csv";
        string[] lines = File.ReadAllLines(path);
        char separator = lines.FirstOrDefault()?[0] ?? '\t';

        var addresses = lines
            .Skip(1)
            .Select(l => l.Split(separator))
            .Select(arr => new Address()
            {
                Country = arr[0],
                Region = arr[1],
                District = arr[2],
                City = arr[3],
                Street = arr[4],
                Number = arr[5],
                Floats = arr[6],
                Postcode = arr[7]
            });

        return addresses;
    }
}
