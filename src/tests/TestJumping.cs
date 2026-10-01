using Godot;
using GdUnit4;
using static GdUnit4.Assertions;

[TestSuite]
public class TestJumping
{
    private Player _player = null!;

    [BeforeTest]
    [RequireGodotRuntime]
    public void BeforeEach()
    {
        _player = AutoFree(new Player());
        _player.ResetJumps();
    }

    [TestCase]
    [RequireGodotRuntime]
    public void PlayerStartsWithMaxJumps()
    {
        AssertThat(_player.jumpsRemaining)
            .IsEqual(_player.maxJumps);
    }

    [TestCase]
    [RequireGodotRuntime]
    public void JumpConsumesOneJump()
    {
        var jumpsBefore = _player.jumpsRemaining;

        _player.Jump();

        AssertThat(_player.jumpsRemaining)
            .IsEqual(jumpsBefore - 1);
    }

    [TestCase]
    [RequireGodotRuntime]
    public void JumpSetsUpwardVelocity()
    {
        _player.Jump();

        AssertThat(_player.Velocity.Y)
            .IsEqual(_player.jumpSpeed);
    }

    [TestCase]
    [RequireGodotRuntime]
    public void PlayerCanJumpUntilNoJumpsRemain()
    {
        var allowedJumps = _player.maxJumps;

        for (var i = 0; i < allowedJumps; i++)
        {
            _player.Jump();
        }

        AssertThat(_player.jumpsRemaining)
            .IsEqual(0);
    }

    [TestCase]
    [RequireGodotRuntime]
    public void PlayerCannotJumpWhenNoJumpsRemain()
    {
        for (var i = 0; i < _player.maxJumps; i++)
        {
            _player.Jump();
        }

        var velocityBefore = _player.Velocity;

        _player.Jump();

        AssertThat(_player.jumpsRemaining)
            .IsEqual(0);

        AssertThat(_player.Velocity)
            .IsEqual(velocityBefore);
    }

    [TestCase]
    [RequireGodotRuntime]
    public void ResetJumpsRestoresMaxJumps()
    {
        _player.Jump();
        _player.Jump();

        _player.ResetJumps();

        AssertThat(_player.jumpsRemaining)
            .IsEqual(_player.maxJumps);
    }


}