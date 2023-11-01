using MontyHall.Model;

namespace MontyHall.Data;

public static class SeedData
{
    public static void Initialize(MontyHallContext db)
    {
        var simulation = new Simulation
        {
            GameAmount = 2,
            Games = new List<Game>{},
            Strategy = "Switch",
            WinRate = "50%",
            CreatedAt = DateTime.Now
        };

        Game[] games = new Game[]
        {
            new Game()
            {
                Strategy = "Switch",
                Result = "Win",
                DoorWithCar = 1,
                FirstPick = 2,
                DoorOpened = 3,
                FinalPick = 1,
                SimulationId = 1,
                Simulation = simulation
            },
            new Game()
            {
                Strategy = "Switch",
                Result = "Lose",
                DoorWithCar = 2,
                FirstPick = 2,
                DoorOpened = 1,
                FinalPick = 3,
                SimulationId = 1,
                Simulation = simulation
            }
        };

        simulation.Games.Add(games[0]);
        simulation.Games.Add(games[1]);
        db.Simulations.Add(simulation);
        db.Games.AddRange(games);
        db.SaveChanges();
    }
}