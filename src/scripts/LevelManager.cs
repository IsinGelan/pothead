
using Godot;

public partial class LevelManager : Node
{

	[Export]
	public int playerInitHp { get; set; } = 100;

	private const int PLAYER_INIT_LIVES = 3;


	// %Hud — requires a unique node named "Hud" in the scene.
	[Export]
	private Hud levelHud;

	// $DeathTimer
	[Export]
	private Timer deathTimer;


	private int playerHp;
	private int playerLives = PLAYER_INIT_LIVES;


	private Player myPlayer;



	public override void _Ready()
	{
		playerHp = playerInitHp;


		// If you don't want these as [Export] fields, you can instead use:
		// levelHud = GetNode("Hud");
		// deathTimer = GetNode<Timer>("DeathTimer");

		PlayerShowHp();
	}

/// <summary>
/// sets a variable for player node.
/// </summary>
/// <param name="player"> is the player. </param>
	public void RegisterPlayer(Player player)
	{
		myPlayer = player;
	}

/// <summary>
/// calculates new player HP when taking damage and redirects if it's less than 0.
/// </summary>
/// <param name="hpAmmount"> ammount of damage the player takes. </param>
	public void PlayerTakeDamage(int hpAmount)
	{
		playerHp -= hpAmount;
		PlayerShowHp();

		if (playerHp <= 0)
		{
			PlayerDie();
		}
	}

/// <summary>
/// redirects to different function in levelHud.
/// </summary>
	public void PlayerShowHp()
	{
		// Assuming Hud is your own C# class with SetHealthWaterLevel().
		levelHud.SetHealthWaterLevel((int)(playerHp / 10.0f));
	}

/// <summary>
/// handels player death, respawn and lifeloss.
/// </summary>
	public void PlayerDie()
	{
		playerLives -= 1;

		// Assuming Hud is your own C# class with Die().
		levelHud.Die(playerLives);

		deathTimer.Start();
	}

/// <summary>
/// redirects to respawn-function.
/// </summary>
	private void OnDeathTimerTimeout()
	{
		// Falscher Funktionsname
		PlayerRespawn();
	}

/// <summary>
/// makes the payer respawn and relodes the scene.
/// </summary>
	public void PlayerRespawn()
	{
		GD.Print("YOU RESPAWNED +++");

		GetTree().ReloadCurrentScene();

		// myPlayer.ReloadCurrentScene();

		PlayerShowHp();
		playerHp = playerInitHp;
	}

}
