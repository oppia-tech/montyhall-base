using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MontyHall.Data;
using MontyHall.Model;

namespace MontyHall.Controllers;

[Route("api/games")]
[ApiController]
public class GamesController : Controller
{
    private readonly MontyHallContext _db;

    public GamesController(MontyHallContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<Game>>> GetGames()
    {
        var games =  await _db.Games.Take(50).ToListAsync();
        return games;
    }

    [HttpGet("{gameId}")]
    public async Task<ActionResult<Game?>> GetGame(int gameId)
    {
        
        Game? game =  await _db.Games.FindAsync(gameId);
        if (game == null)
        {
            return NotFound();
        }

        return game;
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateGame(Game game)
    {
        _db.Games.Add(game);
        
        await _db.SaveChangesAsync();
        return game.Id;
    }
}