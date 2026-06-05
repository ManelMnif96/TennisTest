using Microsoft.AspNetCore.Mvc;
using AtelierTest.Services;
using AtelierTest.DTOs;
using AtelierTest.Models;
using Swashbuckle.AspNetCore.Filters;

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

    /// <summary>
    /// Retourne la liste des joueurs triée par classement.
    /// </summary>
    /// <returns>Liste des joueurs</returns>
    [HttpGet]
    public IActionResult GetPlayers()
    {
        return Ok(_service.GetAll());
    }

    /// <summary>
    /// Récupère un joueur par son identifiant.
    /// </summary>
    /// <param name="id">Identifiant du joueur</param>
    /// <returns>Un joueur</returns>
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var player = _service.GetById(id);

        if (player == null)
            return NotFound(new { message = "Player not found" });

        return Ok(player);
    }

    /// <summary>
    /// Ajouter un nouveau joueur
    /// </summary>
    /// <returns>Joueur ajouté</returns>
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