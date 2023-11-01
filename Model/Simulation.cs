using System.ComponentModel.DataAnnotations;
namespace MontyHall.Model;
public class Simulation {
    public int Id { get; set; }
    public ICollection<Game> Games { get; set; } = new List<Game>();

    [Required(ErrorMessage = "Please enter the desired amount of games")]
    public int GameAmount { get; set; }
    
    [Required(ErrorMessage = "Please select a strategy for the door selection")]
    public string Strategy { get; set; } = "";
    public string WinRate { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.Now;

}
