using IdentityGenerator.Models;
using Microsoft.AspNetCore.Mvc;

namespace IdentityGenerator.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FakeDataController : ControllerBase
{
    [HttpGet]
    public IEnumerable<FakeDataItem> Get(string region, int errorsCount, int itemsCount, int seedNumber)
    {
        return Enumerable.Repeat(new FakeDataItem() { Name = "Some Name", Address = "Some address", Phone = "123 456 789 098" }, itemsCount);
    }
}
