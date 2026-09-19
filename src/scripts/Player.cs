using Godot;
using System.Diagnostics;


public partial class Player : CharacterBody2D
{
	[Export]
	public float HorizontalSpeed { get; set; } = 300.0f;

	[Export]
	public float CrouchSpeed { get; set; } = 150.0f;

	[Export]
	public float JumpSpeed { get; set; } = -600.0f;

	[Export]
	public float Gravity { get; set; } = 980.0f;

	[Export]
	public int OngroundSlowdownSteps { get; set; } = 2;

	[Export]
	public int MaxJumps { get; set; } = 4;


	private Projectiles projectileScene;
	private LevelManager levelManager;

	private int jumpsRemaining;


	// ================================
	// Initialization

	public override void _Ready()
	{
		projectileScene = GetNode<Projectiles>("%Projectiles");
		levelManager = GetNode<LevelManager>("%LevelManager");

		jumpsRemaining = MaxJumps;

		// Equivalent to:
		// level_manager.register_player(self)
		//
		// Uncomment if you want to register the player automatically.
		// levelManager.RegisterPlayer(this);
	}


	public override void _EnterTree()
	{
		// Equivalent to the GDScript _init() assertion.
		Debug.Assert(OngroundSlowdownSteps > 0,
			"Onground Slowdown Steps must be > 0!");

		Input.ActionPress("crouch");
		Input.ActionRelease("crouch");
	}


	// ================================
	// Physics

	public override void _PhysicsProcess(double delta)
	{
		float deltaFloat = (float)delta;

		if (IsOnFloor())
		{
			OngroundMovement(deltaFloat);
		}
		else
		{
			InAirMovement(deltaFloat);
		}

		if (Input.IsActionPressed("crouch"))
		{
			Crouch();
		}
		else if (Input.IsActionJustReleased("crouch"))
		{
			Uncrouch();
		}

		MoveAndSlide();
	}


	// ================================
	// Helpers

	private void ResetJumps()
	{
		jumpsRemaining = MaxJumps;
	}


	private void Jump()
	{
		if (jumpsRemaining == 0)
		{
			return;
		}

		Velocity = new Vector2(Velocity.X, JumpSpeed);
		jumpsRemaining--;
	}


	private bool HorizontalMove()
	{
		// Returns whether the player received movement input.

		float keyPressedDirection =
			Input.GetAxis("walk left", "walk right");

		if (keyPressedDirection != 0)
		{
			if (Input.IsActionPressed("crouch"))
			{
				Velocity = new Vector2(
					keyPressedDirection * CrouchSpeed,
					Velocity.Y
				);
			}
			else
			{
				Velocity = new Vector2(
					keyPressedDirection * HorizontalSpeed,
					Velocity.Y
				);
			}

			return true;
		}

		return false;
	}


	private void SlowDown()
	{
		Velocity = new Vector2(
			Mathf.MoveToward(
				Velocity.X,
				0,
				HorizontalSpeed / OngroundSlowdownSteps
			),
			Velocity.Y
		);
	}


	// ================================
	// Movement functions

	private void OngroundMovement(float delta)
	{
		ResetJumps();

		bool jumping = Input.IsActionJustPressed("jump");

		if (jumping)
		{
			Jump();
		}

		if (!HorizontalMove())
		{
			SlowDown();
		}
	}


	private void InAirMovement(float delta)
	{
		Velocity = new Vector2(
			Velocity.X,
			Velocity.Y + Gravity * delta
		);

		bool jumping = Input.IsActionJustPressed("jump");

		if (jumping)
		{
			Jump();
		}

		HorizontalMove();
	}


	// ================================
	// Shooting

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("shoot"))
		{
			Shoot();
		}
	}


	private void Shoot()
	{
		projectileScene.Shoot(this);
	}


	// ================================
	// Crouching visuals and hitbox

	private void Crouch()
	{
		GetNode<Node2D>("Sprite2D").Visible = false;
		GetNode<Node2D>("cSprite2D").Visible = true;

		GetNode<CollisionShape2D>("CollisionShape2D")
			.SetDeferred("disabled", true);

		GetNode<CollisionShape2D>("cCollision")
			.SetDeferred("disabled", false);
	}


	private void Uncrouch()
	{
		Velocity = new Vector2(
			Velocity.X,
			Velocity.Y - 200
		);

		GetNode<Node2D>("Sprite2D").Visible = true;
		GetNode<Node2D>("cSprite2D").Visible = false;

		GetNode<CollisionShape2D>("CollisionShape2D")
			.SetDeferred("disabled", false);

		GetNode<CollisionShape2D>("cCollision")
			.SetDeferred("disabled", true);
	}
}
