using Godot;

public partial class Heart : Node2D
{
	
/// <summary>
/// makes heart fade out.
/// </summary>
	public void Fade()
	{
		Tween tween = CreateTween();

		tween.TweenProperty(this, "modulate:a", 0.0f, 0.5f);
		tween.TweenCallback(Callable.From(Hide));

		Disappear();
	}

/// <summary>
///makes heart fully dissapear.
/// </summary>
	public void Disappear()
	{
		GD.Print("I disappear!");
		Visible = false;
	}

/// <summary>
///makes heart reapear.
/// </summary>
	public void Reappear()
	{
		Visible = true;
	}
}
