extends GutTest


var projectiles: Node
var player: Player


func before_each() -> void:
	projectiles = Node.new()
	projectiles.set_script(preload("res://src/scripts/projectiles.gd"))

	add_child_autofree(projectiles)

	player = Player.new()
	add_child_autofree(player)

	await get_tree().process_frame


func test_shoot_spawns_projectile() -> void:
	var projectile_count_before := projectiles.get_child_count()

	projectiles.shoot(player)

	assert_eq(
		projectiles.get_child_count(),
		projectile_count_before + 1,
		"Schießen sollte genau ein Geschoss erzeugen."
	)


func test_projectile_spawns_at_player_position() -> void:
	player.position = Vector2(100, 200)

	projectiles.shoot(player)

	var projectile = projectiles.get_child(projectiles.get_child_count() - 1)

	assert_eq(
		projectile.global_position,
		player.position,
		"Das Geschoss sollte am Spieler spawnen."
	)


func test_projectile_flies_towards_aim_direction() -> void:
	player.position = Vector2.ZERO
	player.set_global_mouse_position(Vector2(100, 0))

	projectiles.shoot(player)

	var projectile = projectiles.get_child(projectiles.get_child_count() - 1)

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
