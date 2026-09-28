using Godot;

public partial class Projectiles : Node
{
	private PackedScene projectileScene = GD.Load<PackedScene>(
        "res://src/scenes/projectile.tscn"
	);

	public void Shoot(Player asPlayer)
	{
		var projectile = projectileScene.Instantiate();

<<<<<<< HEAD
        var relativePosition = asPlayer.GetGlobalMousePosition() - asPlayer.GlobalPosition;
        var direction = relativePosition.Normalized();
=======
		var relPos = asPlayer.GetGlobalMousePosition() - asPlayer.GlobalPosition;
		var direction = relPos.Normalized();
>>>>>>> 4e5ca8151f3b81057f891995e6fce4bbfcae5755

		GD.Print(direction);

		((Projectile)projectile).GlobalPosition = asPlayer.GlobalPosition;
		// "Shoot" was spelled "shoot" causing the aiming not to work
		projectile.Call("Shoot", direction);

		AddChild(projectile);
	}
}
