extends CharacterBody2D
class_name Player  # This makes "Player" a recognized type

@export var horizontal_speed: float = 300.0
@export var jump_speed: float = -600.0
@export var gravity: float = 980.0
@export var onground_slowdown_steps: int = 2
@export var max_jumps: int = 4;

var jumps_remaining: int = max_jumps;

func _init() -> void:
	print("Bing!")
	assert(onground_slowdown_steps > 0, "Onground Slowdown Steps must be > 0!")

func _physics_process(delta: float) -> void:
	if is_on_floor():
		onground_movement(delta)
	else:
		in_air_movement(delta)
	
	move_and_slide()

# ================================
# Helpers
func reset_jumps() -> void:
	jumps_remaining = max_jumps
	
func jump() -> void:
	if jumps_remaining == 0:
		return
	velocity.y = jump_speed
	jumps_remaining -= 1

func horizontal_move() -> bool:
	# returns whether player got movement input
	var key_pressed_direction = Input.get_axis("ui_left", "ui_right")
	if key_pressed_direction:
		velocity.x = key_pressed_direction * horizontal_speed
		return true
	return false

func slow_down() -> void:
	velocity.x = move_toward(velocity.x, 0, horizontal_speed / onground_slowdown_steps)

# ================================
# Movement functions
func onground_movement(delta: float) -> void:
	reset_jumps()
	var jumping = Input.is_action_just_pressed("ui_accept")
	if jumping:
		jump()
	if not horizontal_move():
		slow_down()
	
func in_air_movement(delta: float) -> void:
	velocity.y += gravity * delta
	var jumping = Input.is_action_just_pressed("ui_accept")
	if jumping:
		jump()
	horizontal_move()
	
