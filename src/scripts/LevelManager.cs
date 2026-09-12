
using Godot;

public partial class LevelManager : Node
{
    [Export]
    public int PlayerInitHp { get; set; } = 100;

    private const int PlayerInitLives = 3;

    // %Hud — requires a unique node named "Hud" in the scene.
    [Export]
    private Hud levelHud;

    // $DeathTimer
    [Export]
    private Timer deathTimer;

    private int playerHp;
    private int playerLives = PlayerInitLives;

    private Player myPlayer;


    public override void _Ready()
    {
        playerHp = PlayerInitHp;

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

        PlayerShowHp();
        playerHp = PlayerInitHp;
    }
}
