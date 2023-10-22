using IdentityGenerator.Models;

namespace IdentityGenerator.Services
{
    public interface IGenerationProvider
    {
        string Region { get; }
        IEnumerable<FakeDataItem> GenerateFakeItems(int startItem, int itemsCount, int seedNumber);
    }
}
