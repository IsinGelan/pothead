using Godot;

public partial class Heart : Node2D
{
	public void Fade()
	{
		Tween tween = CreateTween();

		tween.TweenProperty(this, "modulate:a", 0.0f, 0.5f);
		tween.TweenCallback(Callable.From(Hide));

		Disappear();
	}

	public void Disappear()
	{
		GD.Print("I disappear!");
		Visible = false;
	}

	public void Reappear()
	{
		Visible = true;
	}
}
