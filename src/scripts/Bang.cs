using Godot;

public partial class Bang : Sprite2D
{
	public void FadeOut()
	{
		var tween = CreateTween();

		tween.TweenProperty(
			this,
			"modulate:a",
			0.0f,
			0.5
		);

		tween.TweenCallback(Callable.From(QueueFree));
	}

	public override void _Ready()
	{
		GD.Print($"Hi! @ {Position}");

		FadeOut();
	}
}
