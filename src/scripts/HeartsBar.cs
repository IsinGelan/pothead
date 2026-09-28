using Godot;

public partial class HeartsBar : Node2D
{
	private Heart heart1;
	private Heart heart2;
	private Heart heart3;

	public override void _Ready()
	{
		heart1 = GetNode<Heart>("Heart1");
		heart2 = GetNode<Heart>("Heart2");
		heart3 = GetNode<Heart>("Heart3");
	}

	public void Reset()
	{
		heart1.Reappear();
		heart2.Reappear();
		heart3.Reappear();
	}

	public void Die(int livesLeft)
	{
		if (livesLeft == 2)
		{
			heart3.Fade();
		}

		if (livesLeft == 1)
		{
			heart2.Fade();
		}

		if (livesLeft == 0)
		{
			heart1.Fade();
		}
	}
}
