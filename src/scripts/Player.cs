using Godot;
using System.Diagnostics;


public partial class Player : CharacterBody2D
{

	[Export]
	public float horizontalSpeed { get; set; } = 300.0f;

	[Export]
	public float crouchSpeed { get; set; } = 150.0f;

	[Export]
	public float jumpSpeed { get; set; } = -600.0f;

	[Export]
	public float gravity { get; set; } = 980.0f;

	[Export]
	public int ongroundSlowdownSteps { get; set; } = 2;

	[Export]
	public int maxJumps { get; set; } = 4;



	private Projectiles projectileScene;
	private LevelManager levelManager;

	public int jumpsRemaining;


	// ================================
	// Initialization

	public override void _Ready()
	{
		projectileScene = GetNode<Projectiles>("%Projectiles");
		levelManager = GetNode<LevelManager>("%LevelManager");

		jumpsRemaining = maxJumps;


		// Equivalent to:
		// level_manager.register_player(self)
		//
		// Uncomment if you want to register the player automatically.
		// levelManager.RegisterPlayer(this);
	}



	public override void _EnterTree()
	{
		// Equivalent to the GDScript _init() assertion.
		Debug.Assert(ongroundSlowdownSteps > 0,
			"Onground Slowdown Steps must be > 0!");


		Input.ActionPress("crouch");
		Input.ActionRelease("crouch");
	}


	// ================================
	// Physics

/// <summary>
/// handels physics of scene.
/// </summary>
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

/// <summary>
/// Resets the ammount of jumps currently available to the player.
/// </summary>
	public void ResetJumps()
	{
		jumpsRemaining = maxJumps;
	}

/// <summary>
/// makes player character jump
/// </summary>
	public void Jump()
	{
		if (jumpsRemaining == 0)
		{
			return;
		}

		Velocity = new Vector2(Velocity.X, jumpSpeed);
		jumpsRemaining--;
	}


/// <summary>
/// interprets player input to change the player character's x-velocity.
/// </summary>
	public bool HorizontalMove()
	{
		// Returns whether the player received movement input.

		float keyPressedDirection =
			Input.GetAxis("walk left", "walk right");


		if (keyPressedDirection != 0)
		{
			if (Input.IsActionPressed("crouch"))
			{
				Velocity = new Vector2(
					keyPressedDirection * crouchSpeed,
					Velocity.Y
				);
			}
			else
			{
				Velocity = new Vector2(
					keyPressedDirection * horizontalSpeed,
					Velocity.Y
				);
			}


			return true;
		}

		return false;
	}


/// <summary>
///makes the player character slow down.
/// </summary>
	private void SlowDown()
	{
		Velocity = new Vector2(
			Mathf.MoveToward(
				Velocity.X,
				0,
				horizontalSpeed / ongroundSlowdownSteps
			),
			Velocity.Y
		);
	}



	// ================================
	// Movement functions

/// <summary>
/// moves the player character based on velocity when on ground.
/// </summary>
/// <param name="delta">delta-t.</param>
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


/// <summary>
/// moves the player character based on velocity when in air.
/// </summary>
/// <param name="delta">delta-t.</param>
	private void InAirMovement(float delta)
	{
		Velocity = new Vector2(
			Velocity.X,
			Velocity.Y + gravity * delta
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
	
/// <summary>
/// checks for input to shoot.
/// </summary>
/// <param name="@event"> is an event. </param>
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("shoot"))
		{
			Shoot();
		}
	}

/// <summary>
/// makes player character shoot a projectile
/// </summary>
	private void Shoot()
	{
		projectileScene.Shoot(this);
	}


	// ================================
	// Crouching visuals and hitbox

/// <summary>
/// makes player character crouch down, changes hitbox and sprite.
/// </summary>
	private void Crouch()
	{
		GetNode<Node2D>("Sprite2D").Visible = false;
		GetNode<Node2D>("cSprite2D").Visible = true;

		GetNode<CollisionShape2D>("CollisionShape2D")
			.SetDeferred("disabled", true);

		GetNode<CollisionShape2D>("cCollision")
			.SetDeferred("disabled", false);
	}

/// <summary>
/// makes player character uncrouch, changes hitbox and sprite.
/// </summary>
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
