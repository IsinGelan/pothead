using Godot;

public partial class Projectile : Area2D
{
<<<<<<< HEAD
    [Export]
    public float projectileSpeed { get; set; } = 1000.0f;
=======
	[Export]
	public float ProjectileSpeed { get; set; } = 1000.0f;
>>>>>>> 4e5ca8151f3b81057f891995e6fce4bbfcae5755

	[Export]
	public float gravityStrength { get; set; } = 300.0f;

	private PackedScene _bangScene =
		GD.Load<PackedScene>("res://src/scenes/bang.tscn");

	private Vector2 _direction = Vector2.Right;
	private Vector2 _velocity = Vector2.Zero;

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

<<<<<<< HEAD
    public void Shoot(Vector2 direction)
    {
        _direction = direction;
        _velocity = _direction * projectileSpeed;
    }
=======
	public void Shoot(Vector2 direction)
	{
		_direction = direction;
		_velocity = _direction * ProjectileSpeed;
	}
>>>>>>> 4e5ca8151f3b81057f891995e6fce4bbfcae5755

	public override void _PhysicsProcess(double delta)
	{
		ApplyGravity(delta);

		Position += _velocity * (float)delta;
	}

	private void ApplyGravity(double delta)
	{
		_velocity.Y += gravityStrength * (float)delta;
	}

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
