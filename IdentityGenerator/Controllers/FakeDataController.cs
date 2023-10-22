using IdentityGenerator.Models;
using IdentityGenerator.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityGenerator.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FakeDataController : ControllerBase
{
    private readonly IEnumerable<IGenerationProvider> _generationProviders;

    public FakeDataController(IEnumerable<IGenerationProvider> generationProviders)
    {
        _generationProviders = generationProviders;
    }

    [HttpGet]
    public ActionResult<IEnumerable<FakeDataItem>> Get(string region, int startItem, int itemsCount, int errorsCount, int seedNumber)
    {
        var generator = _generationProviders.SingleOrDefault(g => g.Region == region);

        if (generator == null)
        {
            return BadRequest();
        }

        var items = generator.GenerateFakeItems(startItem, itemsCount, seedNumber);

        return Ok(items);
    }
}
