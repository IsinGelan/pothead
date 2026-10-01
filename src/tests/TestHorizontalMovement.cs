using Godot;
using GdUnit4;
using static GdUnit4.Assertions;

[TestSuite]
public class TestHorizontalMovement
{
    private Player _player = null!;

    [BeforeTest]
    public void BeforeEach()
    {
        _player = AutoFree(new Player());
        // AddChild(_player);
    }

    [AfterTest]
    public void AfterEach()
    {
        _player.QueueFree();
    }

    [TestCase]
    [RequireGodotRuntime]
    public void NoHorizontalInputDoesNotStartMovement()
    {
        _player.Velocity = new Vector2(0.0f, _player.Velocity.Y);

        _player.HorizontalMove();

        AssertThat(_player.Velocity.X).IsEqual(0.0f);
    }

    [TestCase]
    [RequireGodotRuntime]
    public void MovingLeftSetsNegativeHorizontalVelocity()
    {
        try
        {
            Input.ActionPress("walk left");

            _player.HorizontalMove();
        }
        finally
        {
            Input.ActionRelease("walk left");
        }

        AssertThat(_player.Velocity.X)
            .IsEqual(-_player.horizontalSpeed);
    }

    [TestCase]
    [RequireGodotRuntime]
    public void MovingRightSetsPositiveHorizontalVelocity()
    {
        try
        {
            Input.ActionPress("walk right");

            _player.HorizontalMove();
        }
        finally
        {
            Input.ActionRelease("walk right");
        }

        AssertThat(_player.Velocity.X)
            .IsEqual(_player.horizontalSpeed);
    }

    [TestCase]
    [RequireGodotRuntime]
    public void PressingLeftAndRightAtSameTimeStopsHorizontalInput()
    {
        _player.Velocity = new Vector2(0.0f, _player.Velocity.Y);

        try
        {
            Input.ActionPress("walk left");
            Input.ActionPress("walk right");

            _player.HorizontalMove();
        }
        finally
        {
            Input.ActionRelease("walk left");
            Input.ActionRelease("walk right");
        }

        AssertThat(_player.Velocity.X).IsEqual(0.0f);
    }

    [TestCase]
    [RequireGodotRuntime]
    public void HorizontalMovementIsPossibleInAir()
    {
        bool moved;

        try
        {
            Input.ActionPress("walk right");

            moved = _player.HorizontalMove();
        }
        finally
        {
            Input.ActionRelease("walk right");
        }

        AssertThat(moved).IsTrue();
        AssertThat(_player.Velocity.X)
            .IsEqual(_player.horizontalSpeed);
    }

    [TestCase]
    [RequireGodotRuntime]
    public void CrouchingMakesPlayerSlower()
    {
        try
        {
            Input.ActionPress("crouch");
            Input.ActionPress("walk right");

            _player.HorizontalMove();
        }
        finally
        {
            Input.ActionRelease("crouch");
            Input.ActionRelease("walk right");
        }

        AssertThat(_player.Velocity.X)
            .IsEqual(_player.crouchSpeed);
    }
}
