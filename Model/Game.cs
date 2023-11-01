using System;
using System.Text.Json.Serialization;

namespace MontyHall.Model;
public class Game {
    public int Id { get; set; }
    public string Strategy { get; set; } = "";
    public string Result { get; set; } = "";

    public List<Door> Doors = new List<Door>{
        new Door {Id = 1},
        new Door {Id = 2},
        new Door {Id = 3},
    };
    public int DoorWithCar { get; set; }
    public int FirstPick { get; set; }
    public int DoorOpened { get; set; }
    public int FinalPick { get; set; }

    public int? SimulationId { get; set; }

    [JsonIgnore]
    public Simulation? Simulation { get; set; }

    public bool Play(bool _switchDoor) {
        Random random = new Random();

        this.Setup();

        //Decide the player's first pick
        int randomIndex = random.Next(0, this.Doors.Count);
        this.FirstPick = this.Doors[randomIndex].Id;
        this.Doors.RemoveAt(randomIndex);

        //Open the door that doesn't have the car
        foreach(Door door in this.Doors) {
            if(!door.HasCar) {
                this.DoorOpened = door.Id;
                this.Doors.Remove(door);
                break;
            }
        }

        //Switch door or keep the same depending on strategy chosen
        if(_switchDoor) {
            this.FinalPick = this.Doors[0].Id;
        } else {
            this.FinalPick = this.FirstPick;
        }

        if (this.FinalPick == this.DoorWithCar) {
            this.Result = "Win";
            return true;
        } else {
            this.Result = "Lose";
            return false;
        }
    }

    public void Setup() {
        Random random = new Random();

        //Randomly select the door with the car
        int randomIndex = random.Next(0, this.Doors.Count);
        this.DoorWithCar = this.Doors[randomIndex].Id;
        this.Doors[randomIndex].HasCar = true;
    }
}