using Godot;

public partial class Projectiles : Node
{
    private PackedScene projectileScene = GD.Load<PackedScene>(
        "res://src/scenes/projectile.tscn"
    );

    public void Shoot(Player asPlayer)
    {
        var projectile = projectileScene.Instantiate();

        var relPos = asPlayer.GetGlobalMousePosition() - asPlayer.GlobalPosition;
        var direction = relPos.Normalized();

        GD.Print(direction);

        ((Projectile)projectile).GlobalPosition = asPlayer.GlobalPosition;
        projectile.Call("shoot", direction);

        AddChild(projectile);
    }
}
