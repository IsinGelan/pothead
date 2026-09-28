using Godot;

public partial class HealthPipe : Node2D
{
<<<<<<< HEAD
    private const int WATER_SECTIONS = 10;
=======
	private const int WaterSections = 10;
>>>>>>> 4e5ca8151f3b81057f891995e6fce4bbfcae5755

	private Sprite2D water;
	private Node2D front;

	private Vector2 waterSize;
	private float sectionWidth;
	private float waterLeftOriginal;


	public override void _Ready()
	{
		water = GetNode<Sprite2D>("Water");
		front = GetNode<Node2D>("Front");

<<<<<<< HEAD
        waterSize = water.Texture.GetSize();
        sectionWidth = waterSize.X / WATER_SECTIONS;
=======
		waterSize = water.Texture.GetSize();
		sectionWidth = waterSize.X / WaterSections;
>>>>>>> 4e5ca8151f3b81057f891995e6fce4bbfcae5755

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
