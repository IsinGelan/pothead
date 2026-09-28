using Godot;

public partial class GenericDamageSource : Area2D
{
<<<<<<< HEAD
    [Export]
    public int hpDealt { get; set; } = 17;
=======
	[Export]
	public int HpDealt { get; set; } = 17;
>>>>>>> 4e5ca8151f3b81057f891995e6fce4bbfcae5755

	private LevelManager _levelManager;

	public override void _Ready()
	{
		_levelManager = GetNode<LevelManager>("%LevelManager");

		BodyEntered += OnBodyEntered;
	}

<<<<<<< HEAD
    private void OnBodyEntered(Node2D body)
    {
        _levelManager.PlayerTakeDamage(hpDealt);
    }
=======
	private void OnBodyEntered(Node2D body)
	{
		_levelManager.PlayerTakeDamage(HpDealt);
	}
>>>>>>> 4e5ca8151f3b81057f891995e6fce4bbfcae5755
}
