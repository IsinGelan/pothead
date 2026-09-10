extends GutTest

var level_scene: Node2D
var projectiles_scene: Node
var player: Player


func before_each() -> void:
	var l_scene = load("res://src/levels/test_platform.tscn").instantiate()
	level_scene = add_child_autofree(l_scene)
	assert_not_null(level_scene)
	
	player = level_scene.get_node("Player")
	projectiles_scene = level_scene.get_node("Projectiles")

func test_shoot_spawns_projectile() -> void:
	var projectile_count_before := projectiles_scene.get_child_count()

	projectiles_scene.shoot(player)

	assert_eq(
		projectiles_scene.get_child_count(),
		projectile_count_before + 1,
		"Schießen sollte genau ein Geschoss erzeugen."
	)


func test_projectile_spawns_at_player_position() -> void:
	player.position = Vector2(100, 200)

	projectiles_scene.shoot(player)

	var projectile = projectiles_scene.get_child(projectiles_scene.get_child_count() - 1)

	assert_eq(
		projectile.global_position,
		player.position,
		"Das Geschoss sollte am Spieler spawnen."
	)


func test_projectile_flies_towards_aim_direction() -> void:
	player.position = Vector2.ZERO
	
	#
	var canvas_transform = player.get_viewport().get_canvas_transform()
	var player_screen_pos = canvas_transform * player.global_position
	var aim_target_screen = player_screen_pos + Vector2(100, 0)

	player.get_viewport().warp_mouse(aim_target_screen)
	#
	
	projectiles_scene.shoot(player)

	var projectile = projectiles_scene.get_child(projectiles_scene.get_child_count() - 1)

	assert_eq(
		projectile.direction,
		Vector2.RIGHT,
		"Das Geschoss sollte in Richtung des Zielpunkts fliegen."
	)


func test_projectile_speed_is_faster_than_player() -> void:
	var projectile = preload("res://src/scenes/projectile.tscn").instantiate()
	add_child_autofree(projectile)

	assert_gt(
		projectile.projectile_speed,
		player.horizontal_speed,
		"Das Geschoss muss schneller als der Spieler sein."
	)


func test_projectile_velocity_matches_direction_and_speed() -> void:
	var projectile = preload("res://src/scenes/projectile.tscn").instantiate()
	add_child_autofree(projectile)

	var direction := Vector2(1, 0).normalized()

	projectile.shoot(direction)

	assert_eq(
		projectile.velocity,
		direction * projectile.projectile_speed,
		"Die Geschossgeschwindigkeit sollte Richtung × Geschwindigkeit sein."
	)


func test_projectile_has_no_initial_velocity_before_shooting() -> void:
	var projectile = preload("res://src/scenes/projectile.tscn").instantiate()
	add_child_autofree(projectile)

	assert_eq(
		projectile.velocity,
		Vector2.ZERO,
		"Ein neues Geschoss sollte vor dem Schießen keine Geschwindigkeit haben."
	)
