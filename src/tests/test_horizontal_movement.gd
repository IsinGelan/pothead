extends GutTest


var player: Player


func before_each() -> void:
	player = Player.new()
	add_child_autofree(player)


func test_no_horizontal_input_does_not_start_movement() -> void:
	player.velocity.x = 0.0

	player.horizontal_move()

	assert_eq(
		player.velocity.x,
		0.0,
		"Ohne horizontale Eingabe darf keine Bewegung entstehen."
	)


func test_moving_left_sets_negative_horizontal_velocity() -> void:
	# Input muss für den Test simuliert werden.
	Input.action_press("walk left")

	player.horizontal_move()

	Input.action_release("walk left")

	assert_eq(
		player.velocity.x,
		-player.horizontal_speed,
		"Nach links sollte sich der Spieler mit horizontal_speed bewegen."
	)


func test_moving_right_sets_positive_horizontal_velocity() -> void:
	Input.action_press("walk right")

	player.horizontal_move()

	Input.action_release("walk right")

	assert_eq(
		player.velocity.x,
		player.horizontal_speed,
		"Nach rechts sollte sich der Spieler mit horizontal_speed bewegen."
	)


func test_pressing_left_and_right_at_same_time_stops_horizontal_input() -> void:
	player.velocity.x = 0.0

	Input.action_press("walk left")
	Input.action_press("walk right")

	player.horizontal_move()

	Input.action_release("walk left")
	Input.action_release("walk right")

	assert_eq(
		player.velocity.x,
		0.0,
		"Gleichzeitiges Drücken von links und rechts darf keine Bewegung erzeugen."
	)


func test_horizontal_movement_is_possible_in_air() -> void:
	Input.action_press("walk right")

	var moved := player.horizontal_move()

	Input.action_release("walk right")

	assert_true(
		moved,
		"Die horizontale Bewegung sollte auch in der Luft möglich sein."
	)

	assert_eq(
		player.velocity.x,
		player.horizontal_speed
	)


func test_crouching_makes_player_slower() -> void:
	Input.action_press("crouch")
	Input.action_press("walk right")

	player.horizontal_move()

	Input.action_release("crouch")
	Input.action_release("walk right")

	assert_eq(
		player.velocity.x,
		player.crouch_speed,
		"Beim Ducken muss der Spieler langsamer laufen."
	)
