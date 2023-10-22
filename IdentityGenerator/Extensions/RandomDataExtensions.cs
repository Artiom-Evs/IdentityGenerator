using IdentityGenerator.Data;
using IdentityGenerator.Models;

namespace IdentityGenerator.Extensions;

public static class RandomDataExtensions
{
    public static FirstName GetRandomFirstName(this RegionalDataDbContext context, Random rnd, string region, string gender)
    {
        FirstName? name = null;
        int namesCount = context.FirstNames.Count();

        do
        {
            int randomId = rnd.Next(1, namesCount);
            name = context.FirstNames.FirstOrDefault(name =>
                name.Id == randomId
                && name.Country == region
                && (name.Gender == "MF" || name.Gender == gender));
        } while (name == null);

        return name;
    }

    public static SecondName GetRandomSecondName(this RegionalDataDbContext context, Random rnd, string region, string gender)
    {
        SecondName? name = null;
        int namesCount = context.SecondNames.Count();

        do
        {
            int randomId = rnd.Next(1, namesCount);
            name = context.SecondNames.FirstOrDefault(name =>
                name.Id == randomId
                && name.Country == region
                && (name.Gender == "MF" || name.Gender == gender));
        } while (name == null);

        return name;
    }

    public static Address GetRandomAddress(this RegionalDataDbContext context, Random rnd, string region)
    {
        Address? address = null;
        int addressesCount = context.Addresses.Count();

        do
        {
            int randomId = rnd.Next(1, addressesCount);
            address = context.Addresses.FirstOrDefault(a =>
                a.Id == randomId
                && a.Country == region);
        } while (address == null);

        return address;
    }
}
