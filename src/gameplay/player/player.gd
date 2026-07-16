extends CharacterBody2D
class_name Player  # This makes "Player" a recognized type

@export var speed: float = 300.0
@export var jump_velocity: float = -600.0
@export var gravity: float = 980.0

func _physics_process(delta: float) -> void:
	# Apply gravity
	if not is_on_floor():
		velocity.y += gravity * delta
	
	# Handle jump
	if Input.is_action_just_pressed("ui_accept") and is_on_floor():
		velocity.y = jump_velocity
	
	# Get input direction
	var direction = Input.get_axis("ui_left", "ui_right")
	
	# Apply movement
	if direction:
		velocity.x = direction * speed
	else:
		velocity.x = move_toward(velocity.x, 0, speed)
	
	move_and_slide()
