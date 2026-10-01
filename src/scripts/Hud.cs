using Godot;

public partial class Hud : Node
{
	private HealthPipe healthPipe;
	private HeartsBar heartsBar;

	public override void _Ready()
	{
		healthPipe = GetNode<HealthPipe>("HealthPipe");
		heartsBar = GetNode<HeartsBar>("HeartsBar");
	}

/// <summary>
/// redirects to different function in healthPipe.
/// </summary>
/// <param name="to"> target waterlevel. </param>
	public void SetHealthWaterLevel(int to)
	{
		healthPipe.SetWaterLevel(to);
	}

/// <summary>
///  redirects to dofferent function in heartsBar.
/// </summary>
/// <param name="livesLeft"> ammount of lives player will have left after. </param>
	public void Die(int livesLeft)
	{
		heartsBar.Die(livesLeft);
	}
}
