using Godot;

public partial class Crosshair : Node
{
    private Texture2D _crosshair;

    public override void _Ready()
    {
        // Load the custom mouse cursor.
        _crosshair = GD.Load<Texture2D>(
            "res://assets/textures/crosshair.svg"
        );

        // Change the default arrow cursor.
        Input.SetCustomMouseCursor(_crosshair);

        // To change a specific cursor shape instead:
        // Input.SetCustomMouseCursor(_beam, Input.CursorShape.Ibeam);
    }
}
