using Godot;

public partial class GenericDamageSource : Area2D
{
	[Export]
	public int HpDealt { get; set; } = 17;

	private LevelManager _levelManager;

	public override void _Ready()
	{
		_levelManager = GetNode<LevelManager>("%LevelManager");

		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		_levelManager.PlayerTakeDamage(HpDealt);
	}
}
