using Godot;

public partial class HealthPipe : Node2D
{
	private const int WaterSections = 10;

	private Sprite2D water;
	private Node2D front;

	private Vector2 waterSize;
	private float sectionWidth;
	private float waterLeftOriginal;


	public override void _Ready()
	{
		water = GetNode<Sprite2D>("Water");
		front = GetNode<Node2D>("Front");

		waterSize = water.Texture.GetSize();
		sectionWidth = waterSize.X / WaterSections;

		water.RegionEnabled = true;

		waterLeftOriginal = water.Position.X;

		SetWaterLevel(10);
	}


	public void SetWaterLevel(int to)
	{
		float widthAfter = to * sectionWidth;

		water.RegionRect = new Rect2(
			0,
			0,
			widthAfter,
			waterSize.Y
		);

		// Reposition
		Vector2 position = water.Position;
		position.X = 0.5f * (waterSize.X - widthAfter);
		water.Position = position;
	}
}
