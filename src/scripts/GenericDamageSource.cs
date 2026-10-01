using Godot;

public partial class GenericDamageSource : Area2D
{

	[Export]
	public int hpDealt { get; set; } = 17;


	private LevelManager _levelManager;

	public override void _Ready()
	{
		_levelManager = GetNode<LevelManager>("%LevelManager");

		BodyEntered += OnBodyEntered;
	}

/// <summary>
/// refers function to the level manager.
/// </summary>
/// <param name="body"> the object itself. </param>
	private void OnBodyEntered(Node2D body)
	{
		_levelManager.PlayerTakeDamage(hpDealt);
	}

}
