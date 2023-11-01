using Blazorise;

namespace MontyHall.Model;
public class Door {
    public int Id { get; set; }
    public bool Opened = false;
    public bool HasCar { get; set; }

    public Background BgColor = Background.Default;
    public Color ButtonColor = Color.Primary;
    public string DoorImageSource = "/images/door.png";
    public bool Selected = false;

    public Task Select() {
        this.BgColor = Background.Success;
        this.ButtonColor = Color.Light;
        this.Selected = true;
        return Task.CompletedTask;
    }

    public Task Unselect() {
        this.BgColor = Background.Default;
        this.ButtonColor = Color.Primary;
        this.Selected = false;
        return Task.CompletedTask;
    }

    public Task Open(string newImageSource) {
        this.Selected = true;
        this.Opened = true;
        this.DoorImageSource = newImageSource;

        return Task.CompletedTask;
    }
}
