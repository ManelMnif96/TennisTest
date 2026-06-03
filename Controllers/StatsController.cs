using Microsoft.AspNetCore.Mvc;
using AtelierTest.Services;

namespace AtelierTest.Controllers;

[ApiController]
[Route("stats")]
public class StatsController : ControllerBase
{
    private readonly PlayerService _service;

    public StatsController(PlayerService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetStats()
    {
        return Ok(_service.GetStatistics());
    }
}