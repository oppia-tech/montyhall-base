using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MontyHall.Data;
using MontyHall.Model;

namespace MontyHall.Controllers;

[Route("api/simulations")]
[ApiController]
public class SimulationsController : Controller
{
    private readonly MontyHallContext _db;

    public SimulationsController(MontyHallContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<Simulation>>> GetSimulations()
    {
        
        var simulations =  await _db.Simulations.ToListAsync();
        return simulations;
    }

    [HttpGet("{simulationId}")]
    public async Task<ActionResult<Simulation?>> GetSimulation(int simulationId)
    {
        
        Simulation? simulation =  await _db.Simulations.Where(s  => s.Id == simulationId).Include(s => s.Games.Take(100)).SingleOrDefaultAsync();
        if (simulation == null)
        {
            return NotFound();
        }

        return simulation;
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateSimulation(Simulation simulation)
    {
        _db.Simulations.Add(simulation);
        int winCount = 0;
        List<Game> games = new List<Game>{};
        for (int i = 0 ; i < simulation.GameAmount; i++) {
            Game newGame = new Game{
                Strategy = simulation.Strategy,
                Simulation = simulation
            };
            bool switchDoor = newGame.Strategy == "Switch" ? true : false;
            bool gameWon = newGame.Play(switchDoor);
            if(gameWon) winCount +=1;
            games.Add(newGame);
        }
        await _db.Games.AddRangeAsync(games);
        simulation.WinRate = $"{winCount * 100 / simulation.GameAmount}%";
        await _db.SaveChangesAsync();
        return simulation.Id;
    }
}