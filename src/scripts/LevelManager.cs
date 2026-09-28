
using Godot;

public partial class LevelManager : Node
{
<<<<<<< HEAD
    [Export]
    public int playerInitHp { get; set; } = 100;

    private const int PLAYER_INIT_LIVES = 3;
=======
	[Export]
	public int PlayerInitHp { get; set; } = 100;

	private const int PlayerInitLives = 3;
>>>>>>> 4e5ca8151f3b81057f891995e6fce4bbfcae5755

	// %Hud — requires a unique node named "Hud" in the scene.
	[Export]
	private Hud levelHud;

	// $DeathTimer
	[Export]
	private Timer deathTimer;

<<<<<<< HEAD
    private int playerHp;
    private int playerLives = PLAYER_INIT_LIVES;
=======
	private int playerHp;
	private int playerLives = PlayerInitLives;
>>>>>>> 4e5ca8151f3b81057f891995e6fce4bbfcae5755

	private Player myPlayer;


<<<<<<< HEAD
    public override void _Ready()
    {
        playerHp = playerInitHp;
=======
	public override void _Ready()
	{
		playerHp = PlayerInitHp;
>>>>>>> 4e5ca8151f3b81057f891995e6fce4bbfcae5755

		// If you don't want these as [Export] fields, you can instead use:
		// levelHud = GetNode("Hud");
		// deathTimer = GetNode<Timer>("DeathTimer");

		PlayerShowHp();
	}


	public void RegisterPlayer(Player player)
	{
		myPlayer = player;
	}


	public void PlayerTakeDamage(int hpAmount)
	{
		playerHp -= hpAmount;
		PlayerShowHp();

		if (playerHp <= 0)
		{
			PlayerDie();
		}
	}


	public void PlayerShowHp()
	{
		// Assuming Hud is your own C# class with SetHealthWaterLevel().
		levelHud.SetHealthWaterLevel((int)(playerHp / 10.0f));
	}


	public void PlayerDie()
	{
		playerLives -= 1;

		// Assuming Hud is your own C# class with Die().
		levelHud.Die(playerLives);

		deathTimer.Start();
	}


	private void OnDeathTimerTimeout()
	{
		// Falscher Funktionsname
		PlayerRespawn();
	}


	public void PlayerRespawn()
	{
		GD.Print("YOU RESPAWNED +++");

		GetTree().ReloadCurrentScene();

		// myPlayer.ReloadCurrentScene();

<<<<<<< HEAD
        PlayerShowHp();
        playerHp = playerInitHp;
    }
=======
		PlayerShowHp();
		playerHp = PlayerInitHp;
	}
>>>>>>> 4e5ca8151f3b81057f891995e6fce4bbfcae5755
}
