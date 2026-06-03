using Microsoft.AspNetCore.Mvc;
using AtelierTest.Services;
using AtelierTest.DTOs;

namespace AtelierTest.Controllers;

[ApiController]
[Route("players")]
public class PlayersController : ControllerBase
{
    private readonly PlayerService _service;

    public PlayersController(PlayerService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetPlayers()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var player = _service.GetById(id);

        if (player == null)
            return NotFound(new { message = "Player not found" });

        return Ok(player);
    }

    [HttpPost]
    public IActionResult CreatePlayer([FromBody] CreatePlayerDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var created = _service.AddPlayer(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created
        );
    }
}