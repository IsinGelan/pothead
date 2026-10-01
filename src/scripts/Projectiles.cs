using Godot;

public partial class Projectiles : Node
{
	private PackedScene projectileScene = GD.Load<PackedScene>(
        "res://src/scenes/projectile.tscn"
	);

	public Vector2 GetAimDirection(Player player)
	{
		var relativePosition =
			player.GetGlobalMousePosition() -
			player.GlobalPosition;

		return relativePosition.Normalized();
	}
	
/// <summary>
/// handels origin-position and velocity of player-projectile.
/// </summary>
/// <param name="asPlayer"> the player character node. </param>
	public void Shoot(Player asPlayer)
	{
		var direction = GetAimDirection(asPlayer);
		GD.Print(direction);

		var projectile = (Projectile)projectileScene.Instantiate();
		projectile.GlobalPosition = asPlayer.GlobalPosition;
		// "Shoot" was spelled "shoot" causing the aiming not to work
		projectile.Shoot(direction);

		AddChild(projectile);
	}
}
