using Godot;
using GdUnit4;
using static GdUnit4.Assertions;

[TestSuite]
public class TestShooting
{
    private Node2D _levelScene = null!;
    private Projectiles _projectilesScene = null!;
    private Player _player = null!;

    [BeforeTest]
    [RequireGodotRuntime]
    public void BeforeEach()
    {
        var scene = GD.Load<PackedScene>(
            "res://src/levels/test_platform.tscn"
        );

        _levelScene = AutoFree(scene.Instantiate<Node2D>());

        AssertThat(_levelScene).IsNotNull();

        _player = _levelScene.GetNode<Player>("Player");
        _projectilesScene = (Projectiles)_levelScene.GetNode("Projectiles");

        AssertThat(_player).IsNotNull();
        AssertThat(_projectilesScene).IsNotNull();
    }

    [TestCase]
    [RequireGodotRuntime]
    public void ShootSpawnsProjectile()
    {
        var projectileCountBefore = _projectilesScene.GetChildCount();

        _projectilesScene.Shoot(_player);

        AssertThat(_projectilesScene.GetChildCount())
            .IsEqual(projectileCountBefore + 1);
    }

    [TestCase]
    [RequireGodotRuntime]
    public void ProjectileSpawnsAtPlayerPosition()
    {
        _player.Position = new Vector2(100, 200);

        _projectilesScene.Shoot(_player);

        var projectile = (Projectile)_projectilesScene.GetChild(
            _projectilesScene.GetChildCount() - 1
        );

        AssertThat(projectile.GlobalPosition)
            .IsEqual(_player.Position);
    }

    [TestCase]
    [RequireGodotRuntime]
    public void ProjectileFliesTowardsAimDirection()
    {
        var playerPosition = _player.Position;

        // Put the mouse 100 pixels to the right of the player.
        Input.WarpMouse(playerPosition + new Vector2(100, 0));

        _projectilesScene.Shoot(_player);

        var projectile = (Projectile)_projectilesScene.GetChild(
            _projectilesScene.GetChildCount() - 1
        );

        AssertThat(projectile._direction)
            .IsEqual(Vector2.Right);
    }

    [TestCase]
    [RequireGodotRuntime]
    public void ProjectileSpeedIsFasterThanPlayer()
    {
        var scene = GD.Load<PackedScene>(
            "res://src/scenes/projectile.tscn"
        );

        var projectile = AutoFree(scene.Instantiate<Projectile>());

        var projectileSpeed = projectile.projectileSpeed;

        AssertThat(projectileSpeed)
            .IsGreater(_player.horizontalSpeed);
    }

    [TestCase]
    [RequireGodotRuntime]
    public void ProjectileVelocityMatchesDirectionAndSpeed()
    {
        var scene = GD.Load<PackedScene>(
            "res://src/scenes/projectile.tscn"
        );

        var projectile = AutoFree(
            scene.Instantiate<Projectile>()
        );

        var direction = new Vector2(1, 0).Normalized();

        projectile.Shoot(direction);

        AssertThat(projectile._velocity)
            .IsEqual(direction * projectile.projectileSpeed);
    }

    [TestCase]
    [RequireGodotRuntime]
    public void ProjectileHasNoInitialVelocityBeforeShooting()
    {
        var scene = GD.Load<PackedScene>(
            "res://src/scenes/projectile.tscn"
        );

        var projectile = AutoFree(
            scene.Instantiate<Projectile>()
        );

        AssertThat(projectile._velocity)
            .IsEqual(Vector2.Zero);
    }


}