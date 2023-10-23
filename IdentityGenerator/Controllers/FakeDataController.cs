using CsvHelper;
using CsvHelper.Configuration;
using IdentityGenerator.Models;
using IdentityGenerator.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net.Mime;

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

    [ActionName("csv")]
    [HttpGet("[action]")]
    public IActionResult GetCsvFile(string region, int itemsCount, int errorsCount, int seedNumber)
    {
        var generator = _generationProviders.SingleOrDefault(g => g.Region == region);

        if (generator == null)
        {
            return BadRequest();
        }

        var items = generator.GenerateFakeItems(0, itemsCount, seedNumber);
        var csvConfig = new CsvConfiguration(new CultureInfo(region))
        {
            Delimiter = ";"
        };
        
        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        using var csv = new CsvWriter(writer, csvConfig);
        
        csv.WriteRecords(items);
        
        return File(stream.ToArray(), "text/csv", "fake_data.csv");
    }


}
