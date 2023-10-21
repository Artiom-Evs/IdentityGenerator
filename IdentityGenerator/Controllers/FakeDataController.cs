using IdentityGenerator.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityGenerator.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FakeDataController : ControllerBase
{
    [HttpGet]
    public IEnumerable<FakeDataItem> Get()
    {
        return new FakeDataItem[]
        {
            new() { Name = "Some Name", Address = "Some address", Phone = "123 456 789 098" },
            new() { Name = "Some Name", Address = "Some address", Phone = "123 456 789 098" },
            new() { Name = "Some Name", Address = "Some address", Phone = "123 456 789 098" }
        };
    }
}
