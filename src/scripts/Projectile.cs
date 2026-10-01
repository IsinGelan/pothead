using Godot;

public partial class Projectile : Area2D
{

	[Export]
	public float projectileSpeed { get; set; } = 1000.0f;


	[Export]
	public float gravityStrength { get; set; } = 300.0f;

	private PackedScene _bangScene =
		GD.Load<PackedScene>("res://src/scenes/bang.tscn");

	public Vector2 _direction = Vector2.Right;
	public Vector2 _velocity = Vector2.Zero;

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

/// <summary>
/// makes projectile go in desired direction.
/// </summary>
/// <param name="direction"> desired direction. </param>
	public void Shoot(Vector2 direction)
	{
		_direction = direction;
		_velocity = _direction * projectileSpeed;
	}

/// <summary>
/// applyes physics to projectile.
/// </summary>
/// <param name="delta"> delta-t </param>
	public override void _PhysicsProcess(double delta)
	{
		ApplyGravity(delta);

		Position += _velocity * (float)delta;
	}

/// <summary>
/// applyes the gravity part of physics.
/// </summary>
/// <param name="delta"> delta-t </param>
	private void ApplyGravity(double delta)
	{
		_velocity.Y += gravityStrength * (float)delta;
	}

/// <summary>
/// makes projectile do something upon impact.
/// </summary>
/// <param name="body"> the body of this node </param>
	private void OnBodyEntered(Node2D body)
	{
		// TODO: Do something when impacting something,
		// e.g. a boss.

		var bang = _bangScene.Instantiate<Node2D>();

		bang.GlobalPosition = GlobalPosition;

		GetParent().AddChild(bang);

		// TODO: Apply damage.

		QueueFree();
	}
}
