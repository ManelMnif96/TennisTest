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

    /// <summary>
    /// Retourne les statistiques globales des joueurs.
    /// </summary>
    /// <returns>Stats globales</returns>
    [HttpGet]
    public IActionResult GetStats()
    {
        return Ok(_service.GetStatistics());
    }
}