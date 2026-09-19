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

	public void SetHealthWaterLevel(int to)
	{
		healthPipe.SetWaterLevel(to);
	}

	public void Die(int livesLeft)
	{
		heartsBar.Die(livesLeft);
	}
}
