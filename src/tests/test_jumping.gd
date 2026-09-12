extends GutTest


var player: Player


func before_each() -> void:
	player = Player.new()
	add_child_autofree(player)


func test_player_starts_with_max_jumps() -> void:
	assert_eq(
		player.jumps_remaining,
		player.max_jumps,
		"Der Spieler sollte mit der maximalen Anzahl an Sprüngen starten."
	)


func test_jump_consumes_one_jump() -> void:
	var jumps_before := player.jumps_remaining

	player.jump()

	assert_eq(
		player.jumps_remaining,
		jumps_before - 1,
		"Ein Sprung sollte genau einen verfügbaren Sprung verbrauchen."
	)


func test_jump_sets_upward_velocity() -> void:
	player.jump()

	assert_eq(
		player.velocity.y,
		player.jump_speed,
		"Ein Sprung sollte die vertikale Geschwindigkeit auf jump_speed setzen."
	)


func test_player_can_jump_until_no_jumps_remain() -> void:
	var allowed_jumps := player.max_jumps

	for i in allowed_jumps:
		player.jump()

	assert_eq(
		player.jumps_remaining,
		0,
		"Nach allen verfügbaren Sprüngen sollten keine Sprünge mehr übrig sein."
	)


func test_player_cannot_jump_when_no_jumps_remain() -> void:
	for i in player.max_jumps:
		player.jump()

	var velocity_before := player.velocity

	player.jump()

	assert_eq(
		player.jumps_remaining,
		0,
		"Es dürfen keine weiteren Sprünge verfügbar werden."
	)

	assert_eq(
		player.velocity,
		velocity_before,
		"Ein Sprung darf nicht mehr ausgeführt werden, wenn keine Sprünge verbleiben."
	)


func test_reset_jumps_restores_max_jumps() -> void:
	player.jump()
	player.jump()

	player.reset_jumps()

	assert_eq(
		player.jumps_remaining,
		player.max_jumps,
		"Auf dem Boden sollte die maximale Sprunganzahl wiederhergestellt werden."
	)
